using SFML.System;

namespace Xenon.Common.Utilities {
	/// <summary>
	/// An object that keeps track of the amount of frames between seconds.
	/// </summary>
	public class FPS {
		uint frame, fps;
		Clock clock = new Clock();

		/// <summary>
		/// Basic function loop, used to increment stored frames. 
		/// </summary>
		public void Update() {
			if (clock.ElapsedTime.AsSeconds() >= 1.0f) {
				fps = frame;
				frame = 0;
				clock.Restart();
			}

			++frame;
		}

		/// <summary>
		/// Returns amount of frames stored in the object.
		/// </summary>
		/// <returns>Unsigned Integer</returns>
		public uint GetFPS() { return fps; }
	}
}
