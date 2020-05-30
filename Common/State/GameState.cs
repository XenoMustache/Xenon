using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xenon.Common.Object;

namespace Xenon.Common.State {
	public class GameState : IDisposable {
		public bool pausedUpdate = false, pausedRender = false;

		protected List<GameObject> Objects = new List<GameObject>();

		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
		bool disposed = false, isInitialized = false;

		public virtual void Load() {
			isInitialized = true;
		}

		public virtual void Update(double deltaTime) {
			if (!pausedUpdate && isInitialized) {
				foreach (var obj in Objects) obj.Update(deltaTime);
			}
		}

		public virtual void Render(RenderWindow window) {
			if (!pausedUpdate && isInitialized) {
				foreach (var obj in Objects) obj.Render(window);
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
