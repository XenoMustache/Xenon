using SFML.Graphics;
using SFML.System;
using Xenon.Common.Objects;

namespace Xenon.Client.Objects {
	public class Camera : GameObject {
		public float camZoom;
		public Vector2f target, uiViewSize;

		View camView = new View(), uiView = new View();

		public override void Update(double deltaTime) {
			camView.Center = new Vector2f(target.X, target.Y);
		}

		public override void Render(RenderWindow window) {
			window.SetView(uiView);
			uiView.Size = uiViewSize;

			window.SetView(camView);
			camView.Size = (Vector2f)window.Size;
			camView.Zoom(camZoom);
		}
	}
}
