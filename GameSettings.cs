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
			if (File.Exists("GameSettings.json")) {
				JsonConvert.PopulateObject(File.ReadAllText("GameSettings.json"), this);

				Console.WriteLine("Game settings file found, loading...");
			}
			else {
				Console.WriteLine("No game settings file found, generating...");
			}
		}

		public void Serialize() {
			if (File.Exists("GameSettings.json")) File.Delete("GameSettings.json");

			var json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

			File.WriteAllText("GameSettings.json", json);
			Console.WriteLine($"\n{"GameSettings.json"}:\n{json}");
		}
	}
}
