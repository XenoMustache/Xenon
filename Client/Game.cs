using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Xenon.Common.State;

namespace Xenon.Client {
	public abstract class Game {
		public VideoMode screenSize;

		protected string name;
		protected double deltatime = 0.01, secondsPerFrame = 0.05;
		protected uint depthBits, stencilBits, antialiasingLevel, frameLimit = 60;
		protected StateManager stateManager = new StateManager();
		protected ContextSettings settings;
		protected RenderWindow window;

		double accumulator;

		public Game(string name, Vector2u screenSize) {
			this.name = name;
			this.screenSize = new VideoMode(screenSize.X, screenSize.Y);

			PreInit();
			Run();
		}

		protected virtual void PreInit() { }

		protected void Run() {
			settings = new ContextSettings(depthBits, stencilBits, antialiasingLevel);

			window = new RenderWindow(screenSize, name, Styles.Default, settings);
			window.Closed += (s, e) => window.Close();
			window.Resized += (s, e) => window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
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

			window.Clear(Color.Black);
			window.DispatchEvents();

			while (accumulator >= deltatime) {
				Update(deltatime);
				accumulator -= deltatime;
			}

			Render(window);

			window.Display();
		}

		protected virtual void Update(double deltaTime) { stateManager.currentState.Update(deltaTime); }

		protected virtual void Render(RenderWindow window) { stateManager.currentState.Render(window); }

		protected virtual void Exit() { }
	}
}
