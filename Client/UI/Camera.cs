using SFML.Graphics;
using SFML.System;
using Xenon.Common;

namespace Xenon.Client.UI {
	public class Camera : GameObject {
		public Vector2f target, uiViewSize;

		View camView, uiView;

		public Camera() {
			camView = new View();
			uiView = new View();
		}

		public override void Update() {
			camView.Center = target;
		}

		public override void Render() {
			window.SetView(uiView);
			uiView.Size = uiViewSize;

			window.SetView(camView);
			camView.Size = (Vector2f)window.Size;
		}
	}
}
