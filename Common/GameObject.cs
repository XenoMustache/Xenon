using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using SFML.Graphics;
using System;
using System.Runtime.InteropServices;

namespace Xenon.Common {
	public abstract class GameObject : IDisposable {
		bool disposed;
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		/// <summary>
		/// Called within the game loop, used to control the logic of the object.
		/// </summary>
		public abstract void Update(double deltaTime);

		/// <summary>
		/// Called outside of the game loop, used to control what is rendered onto the game window.
		/// </summary>
		public abstract void Render(RenderWindow window);

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing) {
			if (disposed) return;
			if (disposing) OnDispose();

			disposed = true;
		}

		/// <summary>
		/// Called when the object is disposed, used to control what happens when the object is disposed.
		/// </summary>
		protected virtual void OnDispose() {
			handle.Dispose();
		}
	}
}
