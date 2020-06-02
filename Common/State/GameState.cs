using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xenon.Common.Object;

namespace Xenon.Common.State {
	public class GameState : IDisposable {
		public double deltaTime;
		public bool pausedUpdate = false, pausedRender = false;
		public RenderWindow window;

		protected List<GameObject> Objects = new List<GameObject>();

		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
		bool disposed = false, isInitialized = false;

		public virtual void Init() {
			isInitialized = true;
		}

		public virtual void Update() {
			if (!pausedUpdate && isInitialized) {
				foreach (var obj in Objects) {
					obj.deltaTime = deltaTime;
					obj.Update();
				}
			}
		}

		public virtual void Render() {
			if (!pausedUpdate && isInitialized) {
				foreach (var obj in Objects) {
					obj.window = window;
					obj.Render();
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
