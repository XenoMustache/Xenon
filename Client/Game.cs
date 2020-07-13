using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Xenon.Common.State;
using Xenon.Common.Utilities;
using OpenTK.Graphics.OpenGL;


namespace Xenon.Client {
	public abstract class Game {
		public VideoMode screenSize;

		protected string name;
		protected double deltatime = 0.01, secondsPerFrame = 0.05;
		protected uint depthBits, stencilBits, antialiasingLevel, frameLimit;
		protected bool exportLog = true;
		protected StateManager stateManager = new StateManager();
		protected ContextSettings settings;
		protected RenderWindow window;

		double accumulator;

		public Game(string name, Vector2u screenSize) {
			this.name = name;
			this.screenSize = new VideoMode(screenSize.X, screenSize.Y);

			Run();
		}

		protected virtual void PreInit() { }

		protected void Run() {
			PreInit();
			settings = new ContextSettings(depthBits, stencilBits, antialiasingLevel);

#if OPEN_GL
			var gameWindow = new OpenTK.GameWindow();
#warning OpenGL extensions have been enabled. This feature is heavily expiremental and might break things.
#endif

			window = new RenderWindow(screenSize, name, Styles.Default, settings);
			window.Closed += (s, e) => window.Close();

			window.Resized += (s, e) => {
				window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
#if OPEN_GL
				GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
#endif
			};

			window.SetFramerateLimit(frameLimit);
			window.SetActive(true);

			Init();
			Exit();
		}

		protected virtual void Init() {
#if OPEN_GL
			Logger.Print("OpenGL extensions have been enabled.");
#endif

			Clock clock = new Clock();
			double currentTime = clock.Restart().AsSeconds();

			while (window.IsOpen) Loop(clock, currentTime);
		}

		protected void Loop(Clock clock, double currentTime) {
			double newTime = clock.ElapsedTime.AsSeconds();
			double frameTime = newTime - currentTime;

			if (frameTime > secondsPerFrame) frameTime = secondsPerFrame;
			currentTime = newTime;

			accumulator += frameTime;

			window.DispatchEvents();
#if OPEN_GL
			GL.Clear(ClearBufferMask.DepthBufferBit);
#else
			window.Clear(Color.Black);
#endif

			while (accumulator >= deltatime) {
				Update();
				accumulator -= deltatime;
			}

			Render();
			window.Display();
		}

		protected virtual void Update() {
			if (stateManager.currentState != null) {
				stateManager.currentState.deltaTime = deltatime;
				stateManager.currentState.Update();
			}
		}

		protected virtual void Render() {
			if (stateManager.currentState != null) {
				stateManager.currentState.window = window;
				stateManager.currentState.Render();
			}
		}

		protected virtual void Exit() { if (exportLog) Logger.Export(); }
	}
}
