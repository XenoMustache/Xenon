using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xenon.Common.Utilities;

namespace Xenon.Common.State {
	public class StateManager : IDisposable {
		public GameState currentState, oldState;

		bool disposed = false;
		Dictionary<GameState, int> States = new Dictionary<GameState, int>();
		SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

		// TODO: fix persistence
		public void Goto(int stateId, bool persist = false, bool isDefault = false) {
			if (!persist) {
				if (!isDefault) {
					currentState.Dispose();
				}
			} else {
				oldState = currentState;
				oldState.ForcePause();
			}

			currentState = MiscUtils.KeyByValue(States, stateId);
			currentState.Init();
		}

		public int GetCurrentId() {
			States.TryGetValue(currentState, out int id);
			return id;
		}

		public int GetStateCount() {
			return States.Count - 1;
		}

		public void GotoNext(bool persist = false) {
			if (!persist) {
				currentState.Dispose();
			} else {
				oldState = currentState;
				oldState.ForcePause();
			}

			States.TryGetValue(currentState, out int id);

			var newState = MiscUtils.KeyByValue(States, id += 1);

			currentState = newState;
			currentState.Init();
		}

		public void GoBack(bool persist = false) {
			if (!persist) {
				currentState.Dispose();
			} else {
				oldState = currentState;
				oldState.ForcePause();
			}

			States.TryGetValue(currentState, out int id);

			var newState = MiscUtils.KeyByValue(States, id -= 1);

			currentState = newState;
			currentState.Init();
		}

		public void Return(bool persist = false) {
			if (oldState != null) {
				if (!persist) {
					currentState.Dispose();
				} else {
					var tempState = currentState;
					tempState.ForcePause();
					oldState.ForcePause(false);

					currentState = oldState;
					oldState = tempState;
				}
			} else throw new Exception("Old GameState not defined");
		}

		public void AddState(GameState state, int id) { States.Add(state, id); }

		public void RemoveState(GameState state) {
			States.Remove(state);
			state.Dispose();
		}

		public void Unload() {
			foreach (var state in States.Keys) {
				state.Dispose();
				Dispose();
			}
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
