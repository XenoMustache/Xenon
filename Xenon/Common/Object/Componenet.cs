using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace Xenon.Common.Object {
	public abstract class Componenet : IDisposable {
		bool disposed = false;
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		public Componenet() { }

		public abstract void Update();

		public abstract void Render();

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (disposed) return;
			if (disposing) { handle.Dispose(); }

			disposed = true;
		}
	}
}
