using System.Collections.Generic;

namespace Xenon.Engine {
	public class GameObject {
		public List<string> scripts { get; set; }

		public GameObject() {
			scripts = new List<string>();
		}
	}
}
