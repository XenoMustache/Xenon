using Newtonsoft.Json;
using System;
using System.IO;

namespace Xenon.Engine {
	public class GameState {
		public string script { get; set; }

		readonly GameSettings settings;

		public GameState(GameSettings settings) {
			this.settings = settings;
		}

		public void Deserialize(string file) {
			var str = Path.Combine(settings.gameLocation, "States", file);

			if (File.Exists(str)) {
				JsonConvert.PopulateObject(File.ReadAllText(str), this);

				Console.WriteLine($"\nState \"{file}\" found, loading...");
			}
			else {
				Console.WriteLine($"\nUnable to find state \"{file}\"");
			}
		}
	}
}
