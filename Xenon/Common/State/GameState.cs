using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using Xenon.Common.Object;

namespace Xenon.Common.State {
	public class GameState : IDisposable {
		public ObjectManager objectManager = new ObjectManager();

		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
		bool disposed = false, pausedUpdate = false, pausedRender = false;

		public virtual void Load() { }

		public virtual void Update() {
			if (!pausedUpdate) objectManager.Update();
			else return;
		}

		public virtual void Render() {
			if (!pausedUpdate) objectManager.Render();
			else return;
		}

		public virtual void TogglePause(int objectEvent) {
			switch (objectEvent) {
				case 1: if (!pausedUpdate) pausedUpdate = true; else pausedUpdate = false; break;
				case 2: if (!pausedRender) pausedRender = true; else pausedRender = false; break;
				default: throw new Exception("Event not recognized");
			}
		}

		public virtual void ForcePause() {
			pausedUpdate = true;
			pausedRender = true;
		}

		public virtual void ForceUnpause() {
			pausedUpdate = false;
			pausedRender = false;
		}

		public virtual void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			objectManager.Unload();
			if (disposed) return;
			if (disposing) { handle.Dispose(); }

			disposed = true;
		}
	}
}
