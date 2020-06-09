using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xenon.Common.Object {
	public abstract class GameObject : IDisposable {
		public double deltaTime;
		public RenderWindow window;

		bool disposed;
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		public abstract void Update();

		public abstract void Render();

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing) {
			if (disposed) return;
			if (disposing) OnDispose();

			disposed = true;
		}

		protected virtual void OnDispose() {
			handle.Dispose();
		}
	}
}
