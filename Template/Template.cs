using OpenTK;
using System;
using Xenon.Client;
using OpenTK.Graphics.OpenGL4;

namespace Template {
	class Template : Game {

		public Template(int width, int height, string title) : base(width, height, title) { }

		protected override void LoadEvent(EventArgs e) {
			stateManager.AddState(new Triangle(), 0);
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
			stateManager.currentState.Update();
		}
	}
}
