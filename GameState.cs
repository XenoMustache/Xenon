using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Xenon.Engine {
	public class GameState {
		public List<GameObjectInstance> objects { get; set; }

		readonly GameSettings settings;

		public GameState(GameSettings settings) {
			this.settings = settings;

			objects = new List<GameObjectInstance>();
		}

		public void Serialize(string file) {
			if (File.Exists(Path.Combine(settings.gameLocation, "States", file))) File.Delete(Path.Combine(settings.gameLocation, "States", file));

			var json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

			File.WriteAllText(Path.Combine(settings.gameLocation, "States", file), json);
			Console.WriteLine($"\n{file}:\n{json}");
		}

		public void Deserialize(string file) {
			var str = "";

			if (settings.gameLocation != null)
				str = Path.Combine(settings.gameLocation, "States", file);
			else
				str = Path.Combine("States", file);

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
