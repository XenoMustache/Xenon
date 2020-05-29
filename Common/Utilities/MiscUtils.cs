using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xenon.Common.Utilities {
	public static class MiscUtils {
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		const int SW_HIDE = 0;
		const int SW_SHOW = 5;

		public static void HideConsole(bool setting = true) {
			if (setting) {
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_HIDE);
			} else {
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_SHOW);
			}
		}

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
