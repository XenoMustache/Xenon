using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using Xenon.Common.State;

namespace Xenon.Client {
	public abstract class Game {
		public VideoMode screenSize;
		public static bool isFocused = true;

		protected string name;
		protected double deltatime = 0.01, secondsPerFrame = 0.05;
		protected uint depthBits, stencilBits, antialiasingLevel, frameLimit;
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

			window = new RenderWindow(screenSize, name, Styles.Default, settings);
			window.Closed += (s, e) => window.Close();
			window.Resized += (s, e) => window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
			window.GainedFocus += (s, e) => isFocused = true;
			window.LostFocus += (s, e) => isFocused = false;
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
				Update();
				accumulator -= deltatime;
			}

			Render();
			window.Display();
		}

		protected virtual void Update() {
			stateManager.currentState.deltaTime = deltatime;
			stateManager.currentState.Update();
		}

		protected virtual void Render() {
			stateManager.currentState.window = window;
			stateManager.currentState.Render();
		}

		protected virtual void Exit() { }
	}
}
