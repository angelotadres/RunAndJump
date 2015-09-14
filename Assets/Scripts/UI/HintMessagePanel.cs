using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RunAndJump {

	public class HintMessagePanel : MonoBehaviour {

		public GameObject Container;
		public Text MessageValue;

		private void Awake() {
			Container.SetActive(false);
		}

		public void SetMessage(string message) {
			MessageValue.text = message;
		}

		public void Show() {
			Container.SetActive(true);
		}

		public void Hide() {
			Container.SetActive(false);
		}
	}
}
