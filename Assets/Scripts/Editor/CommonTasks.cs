using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RunAndJump {

	public class CommonTasks {

		[MenuItem("Run And Jump/Start Game")]
		public static void PlayIntroScene() {
			string introScenePath = EditorBuildSettings.scenes[0].path;
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			EditorSceneManager.OpenScene(introScenePath);
			EditorApplication.isPlaying = true;
		}
	}
}