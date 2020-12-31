using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Xenon.Engine {
	public class GameSettings {
		public string name { get; set; }
		public string gameLocation { get; set; }

		public List<string> stateIndex { get; set; }

		public GameSettings() {
			name = "Game Title";
			stateIndex = new List<string>();
		}

		public void Deserialize() {
			if (File.Exists("Config\\GameSettings.json")) {
				JsonConvert.PopulateObject(File.ReadAllText("Config\\GameSettings.json"), this);

				Console.WriteLine("\nGame settings file found, loading...");
			}
			else {
				Console.WriteLine("\nNo game settings file found, generating...");
			}
		}

		public void Serialize() {
			if (File.Exists("Config\\GameSettings.json")) File.Delete("Config\\GameSettings.json");

			var json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

			File.WriteAllText("Config\\GameSettings.json", json);
			Console.WriteLine($"\n{"GameSettings.json"}:\n{json}");
		}
	}
}
