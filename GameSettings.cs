using Newtonsoft.Json;
using System.Collections.Generic;

namespace Xenon.Engine {
	public class GameSettings {
		public string name = "Game Title", gameLocation;

		[JsonProperty]
		public List<string> stateIndex = new List<string>();
	}
}
