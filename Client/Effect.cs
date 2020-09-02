using SFML.Graphics;
using Xenon.Common.Utilities;

namespace Xenon.Client {
	public abstract class Effect : Drawable {
		public string name { get; private set; }

		public Effect(string name) {
			this.name = name;
		}

		protected abstract void OnUpdate(float time, float x, float y);
		protected abstract void OnDraw(RenderTarget target, RenderStates states);

		public void Update(float time, float x, float y) {
			if (Shader.IsAvailable) {
				OnUpdate(time, x, y);
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
