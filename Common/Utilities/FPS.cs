using SFML.System;

namespace Xenon.Common.Utilities {
	public class FPS {
		uint frame, fps;
		Clock clock = new Clock();

		public void Update() {
			if (clock.ElapsedTime.AsSeconds() >= 1.0f) {
				fps = frame;
				frame = 0;
				clock.Restart();
			}

			++frame;
		}

		public uint getFPS() { return fps; }
	}
}
