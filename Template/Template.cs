using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;
using Xenon.Client;

using Template.States;

namespace Template {
	class Template : Game {
		KeyboardState lastKeyState;

		public Template(int width, int height, string title) : base(width, height, title) { }

		protected override void LoadEvent(EventArgs e) {
			stateManager.AddState(new Triangle(), 0);
			stateManager.AddState(new ElementBufferObjects(), 1);
			stateManager.AddState(new Textures(), 2);
			stateManager.Goto(0, false, true);
		}

		protected override void RenderEvent(FrameEventArgs e) {
			stateManager.currentState.Render();
			Context.SwapBuffers();
		}

		protected override void ResizeEvent(EventArgs e) {
			GL.Viewport(0, 0, Width, Height);
		}

		protected override void UnloadEvent(EventArgs e) { }

		protected override void UpdateEvent(FrameEventArgs e) {
			var input = Keyboard.GetState();

			if (input.IsKeyDown(Key.Right) && lastKeyState.IsKeyUp(Key.Right) && stateManager.GetCurrentId() < stateManager.GetStateCount()) {
				stateManager.GotoNext();
			}

			if (input.IsKeyDown(Key.Left) && lastKeyState.IsKeyUp(Key.Left) && stateManager.GetCurrentId() > 0) {
				stateManager.GoBack();
			}

			lastKeyState = input;
		}
	}
}
