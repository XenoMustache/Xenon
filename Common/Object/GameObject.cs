using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xenon.Common.Object {
	public abstract class GameObject : IDisposable {
		protected List<Componenet> components = new List<Componenet>();

		bool disposed;
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		public virtual void Update(double deltaTime) {
			foreach (var component in components) component.Update(deltaTime);
		}

		public virtual void Render(RenderWindow window) {
			foreach (var component in components) component.Render(window);
		}

		public virtual void AddComponent(Componenet component) { components.Add(component); }

		public virtual void RemoveComponent(Componenet component) {
			components.Remove(component);
			component.Dispose();
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			foreach (var componenet in components) componenet.Dispose();
			if (disposed) return;
			if (disposing) { handle.Dispose(); }

			disposed = true;
		}
	}
}
