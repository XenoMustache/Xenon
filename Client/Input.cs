using SFML.Graphics;
using SFML.Window;

namespace Xenon.Client {
	public static class Input {
		/// <summary>
		/// Returns true if the game screen is currently focused and false otherwise.
		/// </summary>
		public static bool isFocused = true;
		public static RenderWindow window;
		public static Keyboard.Key lastKeyPressed, lastKeyReleased;

		/// <summary>
		/// Returns an int if the key is pressed and the window is focused.
		/// </summary>
		/// <param name="key"></param>
		/// <returns>Int</returns>
		public static int GetKey(Keyboard.Key key) {
			return (Keyboard.IsKeyPressed(key) ? 1 : 0) * (isFocused ? 1 : 0);
		}

		/// <summary>
		/// Returns an int once if the key is down and the window is focused.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static int GetKeyDown(Keyboard.Key key) {
			if (lastKeyPressed == key && isFocused) return 1; else return 0;
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

		/// <summary>
		/// Returns a bool once if the key is down and the window is focused.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="isBool"></param>
		/// <returns></returns>
		public static bool GetKeyDown(Keyboard.Key key, bool isBool) {
			if (lastKeyPressed == key && isFocused) return true; else return false;
		}
	}
}
