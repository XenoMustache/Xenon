using SFML.Graphics;
using SFML.Window;
using System;

namespace Xenon.Client {
	public class Input {
		/// <summary>
		/// Returns true if the game screen is currently focused and false otherwise.
		/// </summary>
		public static bool isFocused = true;
		public static RenderWindow window;

		/// <summary>
		/// Returns an int if the key is pressed and the window is focused.
		/// </summary>
		/// <param name="key"></param>
		/// <returns>Int</returns>
		public static int GetKey(Keyboard.Key key) {
			return (Keyboard.IsKeyPressed(key) ? 1 : 0) * (isFocused ? 1 : 0);
		}

		/// <summary>
		/// Returns a bool if the key is pressed and the window is focused.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="isBool"></param>
		/// <returns>Boolean</returns>
		public static bool GetKey(Keyboard.Key key, bool isBool) {
			if (isFocused) return Keyboard.IsKeyPressed(key); else return false;
		}
	}
}
