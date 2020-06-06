using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Runtime.InteropServices;

namespace Xenon.Common.Object {
	[Obsolete("Component system is being phased out, move all component logic to parent GameObjects")]
	public abstract class Componenet : IDisposable {
		bool disposed = false;
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		public Componenet() { }

		public abstract void Update(double deltaTime);

		public abstract void Render(RenderWindow window);

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
