using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xenon.Common.State {
	public class GameState : IDisposable {
		public bool pausedUpdate = false, pausedRender = false;

		protected List<GameObject> Objects = new List<GameObject>();

		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
		bool disposed = false, isInitialized = false;

		public GameState(RenderWindow window) { isInitialized = true; }

		public virtual void Update(double deltaTime) {
			if (!pausedUpdate && isInitialized) {
				for (var i = 0; i < Objects.Count; i++) {
					if (!Objects[i].disposed) Objects[i].Update(deltaTime);
				}
			}
		}

		public virtual void Render(RenderWindow window) {
			if (!pausedUpdate && isInitialized) {
				for (var i = 0; i < Objects.Count; i++) {
					if (!Objects[i].disposed) Objects[i].Render(window);
				}
			}
		}

		public virtual void ForcePause(bool set = true) {
			pausedUpdate = set;
			pausedRender = set;
		}

		public virtual void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			foreach (var obj in Objects) obj.Dispose();

			isInitialized = false;
			if (disposed) return;
			if (disposing) { handle.Dispose(); }

			disposed = true;
		}
	}
}
