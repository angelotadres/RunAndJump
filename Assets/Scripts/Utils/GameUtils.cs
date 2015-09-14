using UnityEngine;
using System.Collections;

namespace RunAndJump {

	public class GameUtils {

		public static string TimeFormat(int timeInSeconds) {
			timeInSeconds = (timeInSeconds < 0) ? 0 : timeInSeconds;
			string minutes = (timeInSeconds / 60).ToString();
			string seconds = (timeInSeconds % 60).ToString();
			seconds = (seconds.Length == 1) ? ("0" + seconds) : seconds; 
			return string.Format("{0}:{1}", minutes, seconds);
		}
	}

}
