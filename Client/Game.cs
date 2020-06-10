using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Xenon.Common.State;

namespace Xenon.Client {
	public abstract class Game {
		/// <summary>
		/// Defines a default video mode for your game. See SFML definition for VideoMode;
		/// </summary>
		public VideoMode screenSettings;

		/// <summary>
		/// Defines the string shown on the titlebar of the game window.
		/// </summary>
		protected string name;
		/// <summary>
		/// Represents the time between update calls, generally used for FPS independent math.
		/// </summary>
		protected double deltatime = 0.01;
		/// <summary>
		/// Defines how much time will pass between update calls, can be used to control the speed of your game.
		/// </summary>
		protected double secondsPerFrame = 0.05;
		/// <summary>
		/// Manages GameStates of the game, can be used to move to and from other states.
		/// </summary>
		protected StateManager stateManager = new StateManager();
		/// <summary>
		/// Defines settings related to the game window, see SFML definiton for ContextSettings.
		/// </summary>
		protected ContextSettings settings;
		/// <summary>
		/// Represents the game window, see SFML definition for RenderWindow.
		/// </summary>
		protected RenderWindow window;

		double accumulator;

		/// <summary>
		/// Game constructor. Used to initialize the game into memory.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="screenSize"></param>
		public Game(string name, Vector2u screenSize) {
			this.name = name;
			screenSettings = new VideoMode(screenSize.X, screenSize.Y);

			Run();
		}

		/// <summary>
		/// Called before the game is initialized and the primary loop begins.
		/// </summary>
		protected virtual void PreInit() { }

		/// <summary>
		/// Controls the primary logic of the game and cannot be changed.
		/// </summary>
		protected void Run() {
			PreInit();
			settings = new ContextSettings();

			window = new RenderWindow(screenSettings, name, Styles.Default, settings);
			window.Closed += (s, e) => window.Close();
			window.Resized += (s, e) => window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
			window.GainedFocus += (s, e) => Input.isFocused = true;
			window.LostFocus += (s, e) => Input.isFocused = false;
			window.KeyPressed += (s, e) => Input.lastKeyPressed = e.Code;
			window.KeyReleased += (s, e) => Input.lastKeyReleased = e.Code;
			window.SetActive(true);

			Input.window = window;

			Init();
			Exit();
		}

		/// <summary>
		/// Called as the game is initialized and starts the primary loop.
		/// </summary>
		protected virtual void Init() {
			Clock clock = new Clock();
			double currentTime = clock.Restart().AsSeconds();

			while (window.IsOpen) Loop(clock, currentTime);
		}

		/// <summary>
		/// Primary game loop.
		/// </summary>
		/// <param name="clock"></param>
		/// <param name="currentTime"></param>
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

		/// <summary>
		/// Called within the game loop, used to control state and object logic.
		/// </summary>
		protected virtual void Update() {
			stateManager.currentState.deltaTime = deltatime;
			stateManager.currentState.Update();

			Input.lastKeyPressed = Keyboard.Key.Unknown;
			Input.lastKeyReleased = Keyboard.Key.Unknown;
		}

		/// <summary>
		/// Called outside of the game loop, used to control what is drawn onto the game window.
		/// </summary>
		protected virtual void Render() {
			stateManager.currentState.window = window;
			stateManager.currentState.Render();
		}

		/// <summary>
		/// Called when the game exits, can be used to dispose of any data and gracefully exit/
		/// </summary>
		protected virtual void Exit() { }
	}
}
