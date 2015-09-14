using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RunAndJump {

	public class GoalMenuPanel : MonoBehaviour {

		public Text TimeValue;
		public Text ScoreValue;
		public Text TreasuresValue;
		public GameObject NextButton;

		public void SetScore(int score) {
			ScoreValue.text = score.ToString("#,##0");
		}
		
		public void SetTime(int seconds) {
			TimeValue.text =  GameUtils.TimeFormat(seconds);
		}

		public void SetTreasures(int totalTreasures) {
			TreasuresValue.text = totalTreasures.ToString();
		}

		public void EnableNext(bool enabled) {
			NextButton.SetActive(enabled);
		}
	}
}
