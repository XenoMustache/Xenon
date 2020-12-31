using Newtonsoft.Json;
using System.IO;

namespace Xenon.Engine {
	public class GameObjectInstance {
		public string id { get; set; }

		public GameObject Instantiate(GameSettings settings) {
			var str = "";

			if (settings.gameLocation != null)
				str = Path.Combine(settings.gameLocation, "Objects", id);
			else
				str = Path.Combine("Objects", id);

			var newObject = new GameObject();
			JsonConvert.PopulateObject(File.ReadAllText(str), newObject);

			return newObject;
		}
	}
}
