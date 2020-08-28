using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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

		public static float GetDistance(this Vector2f target1, Vector2f target2) {
			return (float)Math.Abs(Math.Sqrt(((target1.X - target2.X) * (target1.X - target2.X)) + ((target1.Y - target2.Y) * (target1.Y - target2.Y))));
		}

		public static float DegToRad(this float input) {
			return input * (float)Math.PI / 180;
		}

		public static float RadToDeg(this float input) {
			return input * 180 / (float)Math.PI;
		}

		public static float GetDirection(this Vector2f target1, Vector2f target2) {
			return (float)Math.Atan2(target2.Y - target1.Y, target2.X - target1.X);
		}

		public static int FindTurnSideDeg(this float input, float target) {
			var diff = input - target;
			if (diff < 0) diff += 360;

			if (diff > 180) 
				return -1;
			else 
				return 1;
		}
	}
}
