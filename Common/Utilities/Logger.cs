using System;
using System.Collections.Generic;
using System.IO;

namespace Xenon.Common.Utilities {
	public static class Logger {
		static List<string> logList = new List<string>();

		public static void Print(string message, bool showTimestamp = true) {
			string time;
			if (showTimestamp) time = DateTime.Now.ToString("HH:mm:ss") + ": ";
			else time = null;

			Console.WriteLine(time + message);
			logList.Add(time + message);
		}

		public static void Export() {
			string[] log = logList.ToArray();
			string destination = Directory.GetCurrentDirectory() + "\\logs\\", fileName = DateTime.Now.ToString("MM-dd-yyyy -- HH:mm") + ".log";

			if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
			File.WriteAllLines(destination + fileName, log);

			if (File.Exists(destination + "latest.log")) File.Delete(destination + "latest.log");
			File.WriteAllLines(destination + "latest.log", log);
		}
	}
}
