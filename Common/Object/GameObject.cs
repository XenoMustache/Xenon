using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xenon.Common.Object {
	public abstract class GameObject : IDisposable {
		protected List<Componenet> components = new List<Componenet>();
		protected bool pausedUpdate = false, pausedRender = false;

		bool disposed;
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		public virtual void Update(double deltaTime) {
			if (!pausedUpdate)
				foreach (var component in components) component.Update(deltaTime);
			else return;
		}

		public virtual void Render(RenderWindow window) {
			if (!pausedRender)
				foreach (var component in components) component.Render(window);
			else return;
		}

		public virtual void AddComponent(Componenet component) { components.Add(component); }

		public virtual void RemoveComponent(Componenet component) {
			components.Remove(component);
			component.Dispose();
		}

		public virtual void TogglePause(string objectEvent) {
			switch (objectEvent) {
				case "update": if (!pausedUpdate) pausedUpdate = true; else pausedUpdate = false; break;
				case "draw": if (!pausedRender) pausedRender = true; else pausedRender = false; break;
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
