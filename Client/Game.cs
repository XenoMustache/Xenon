#define OPEN_GL

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Xenon.Common.State;
using Xenon.Common.Utilities;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;

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

		protected IGraphicsContext glCtx;

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

#if (OPEN_GL)
			var gameWindow = new OpenTK.GameWindow();
			glCtx = gameWindow.Context;
			gameWindow.Load += (s,e) => GLInit();
			gameWindow.RenderFrame += (s, e) => GLRender();
			Console.WriteLine("OpenGL enabled");
#endif

			window = new RenderWindow(screenSize, name, Styles.Default, settings);
			window.Closed += (s, e) => window.Close();

			window.Resized += (s, e) => {
				window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
#if (OPEN_GL)
				GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
#endif
			};

			window.SetFramerateLimit(frameLimit);
			window.SetActive(true);

			Init();
			Exit();
		}

		protected virtual void Init() {
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

#if (OPEN_GL)
			
#else
			window.Clear(Color.Black);
#endif
		}

		protected virtual void Exit() { if (exportLog) Logger.Export(); }

		protected virtual void GLInit() { }

		protected virtual void GLRender() { }
	}
}
