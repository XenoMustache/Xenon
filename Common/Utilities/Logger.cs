using System;
using System.Collections.Generic;
using System.IO;

namespace Xenon.Common.Utilities {
	public static class Logger {
		static List<string> logList = new List<string>();

		/// <summary>
		/// Prints a string to the console and stores it in memory. Can be configured to show a timestamp.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="showTimestamp"></param>
		public static void Print(string message, bool showTimestamp = true) {
			string time;
			if (showTimestamp) time = DateTime.Now.ToString("[HH:mm:ss] ");
			else time = null;

			Console.WriteLine(time + message);
			logList.Add(time + message);
		}

		/// <summary>
		/// Prints all stored Logger strings into a file in the specified path.
		/// </summary>
		/// <param name="directory"></param>
		public static void Export(string directory = "logs") {
			string[] log = logList.ToArray();
			string destination = Path.Combine(Directory.GetCurrentDirectory(), directory), fileName = DateTime.Now.ToString("MM.dd.yyyy-HH.mm.ss") + ".log";

			if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);

			File.WriteAllLines(destination + "\\" + fileName, log);

			if (File.Exists(destination + "\\latest.log")) File.Delete(destination + "latest.log");
			File.WriteAllLines(destination + "\\latest.log", log);
		}
	}
}
