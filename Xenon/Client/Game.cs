using System;
using OpenTK;
using OpenTK.Graphics;
using Xenon.Common.State;

namespace Xenon.Client {
	public abstract class Game : GameWindow {

		public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { Run(60.0); }
		public static readonly string xenonVer = "0.1";

		protected StateManager stateManager = new StateManager();

		~Game() { Exit(); }

		protected abstract void LoadEvent(EventArgs e);
		protected abstract void UpdateEvent(FrameEventArgs e);
		protected abstract void RenderEvent(FrameEventArgs e);
		protected abstract void ResizeEvent(EventArgs e);
		protected abstract void UnloadEvent(EventArgs e);

		protected override void OnLoad(EventArgs e) {
			LoadEvent(e);
			base.OnLoad(e); 
		}

		protected override void OnUpdateFrame(FrameEventArgs e) {
			UpdateEvent(e);
			base.OnUpdateFrame(e); 
		}

		protected override void OnRenderFrame(FrameEventArgs e) {
			RenderEvent(e);
			base.OnRenderFrame(e); 
		}

		protected override void OnResize(EventArgs e) {
			ResizeEvent(e);
			base.OnResize(e); 
		}

		protected override void OnUnload(EventArgs e) {
			UnloadEvent(e);
			stateManager.Unload();
			base.OnUnload(e); 
		}
	}
}
