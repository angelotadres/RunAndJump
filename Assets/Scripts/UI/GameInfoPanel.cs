using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RunAndJump {
	
	public class GameInfoPanel : MonoBehaviour {

		public Text LivesValue;
		public Text ScoreValue;
		public Text TimeValue;

		public void SetLives(int lives) {
			LivesValue.text = lives.ToString();
		}

		public void SetScore(int score) {
			ScoreValue.text = score.ToString("#,##0");
		}

		public void SetTime(int seconds) {
			TimeValue.text =  GameUtils.TimeFormat(seconds);
		}
	}
}