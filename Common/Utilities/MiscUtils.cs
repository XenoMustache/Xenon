using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xenon.Common.Utilities {
	/// <summary>
	/// Standard utilities to control many aspects of the game.
	/// </summary>
	public static class MiscUtils {
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		const int SW_HIDE = 0;
		const int SW_SHOW = 5;

		/// <summary>
		/// Used to hide or show the console window that appears when the game is run.
		/// </summary>
		/// <param name="setting"></param>
		public static void HideConsole(bool setting = true) {
			if (setting) {
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_HIDE);
			} else {
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_SHOW);
			}
		}

		/// <summary>
		/// Used to find the key of a dictionary based on the stored value. Breaks if there are two of the same values found.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="W"></typeparam>
		/// <param name="dict"></param>
		/// <param name="val"></param>
		/// <returns>TKey</returns>
		public static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val) {
			T key = default;
			foreach (KeyValuePair<T, W> pair in dict) {
				if (EqualityComparer<W>.Default.Equals(pair.Value, val)) {
					key = pair.Key;
					break;
				}
			}
			return key;
		}
	}
}
