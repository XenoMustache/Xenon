using SFML.Graphics;
using Xenon.Common.Utilities;

namespace Xenon.Client {
	public abstract class Effect : Drawable {
		public string name { get; private set; }

		protected abstract void OnUpdate(double deltaTime);
		protected abstract void OnDraw(RenderTarget target, RenderStates states);

		public void Update(double deltaTime) {
			if (Shader.IsAvailable) {
				OnUpdate(deltaTime);
			}
		}

		void Drawable.Draw(RenderTarget target, RenderStates states) {
			if (Shader.IsAvailable) {
				OnDraw(target, states);
			} else {
				Logger.Print("Shader not supported", true, "[ERR]");
			}
		}
	}
}
