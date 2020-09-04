namespace Xenon.Common.Objects {
	public abstract class AIState {
		public AIController controller;

		public abstract void Exectute(double deltaTime);
	}
}
