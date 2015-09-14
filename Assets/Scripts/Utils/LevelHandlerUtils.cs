using UnityEngine;
using System.Collections;

namespace RunAndJump {

	public class LevelHandlerUtils : MonoBehaviour {

		private static Level _level;

		public static IEnumerator LoadLevel(string sceneName) {
			Application.LoadLevelAdditive(sceneName);
			yield return 0;
			_level = GameObject.FindObjectOfType<Level>();
		}

		public static void DestroyLevel() {
			if(_level != null) {
				Destroy(_level.gameObject);
			}
		}
	}
}
