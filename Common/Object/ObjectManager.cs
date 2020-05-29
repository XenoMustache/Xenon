using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xenon.Common.Object {
	public class ObjectManager : IDisposable {
		List<GameObject> Objects = new List<GameObject>();
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
		bool disposed = false;

		public void Update(double deltaTime) { foreach (var Object in Objects) Object.Update(deltaTime); }

		public void Render(RenderWindow window) { foreach (var Object in Objects) Object.Render(window); }

		public void Unload() {
			foreach (var Object in Objects) {
				Objects.Remove(Object);
				Object.Dispose();
				Dispose();
			}
		}

		public void AddObject(GameObject Object) { Objects.Add(Object); }

		public void RemoveObject(GameObject Object) {
			Objects.Remove(Object);
			Object.Dispose();
		}
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
