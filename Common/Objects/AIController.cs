using Xenon.Common.Utilities;

namespace Xenon.Common.Objects {
	public class AIController : GameObject {
		public GameObject parent;
		public AIState currentState;

		public AIController(GameObject parent) {
			this.parent = parent;
		}

		public override void Update(double delatime) {
			currentState.controller = this;
			currentState.Exectute(delatime);
		}
	}
}
