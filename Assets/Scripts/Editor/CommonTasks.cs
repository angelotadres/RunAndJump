using UnityEditor;
using UnityEngine;

namespace RunAndJump {

	public class CommonTasks {

		[MenuItem("Run And Jump/Start Game")]
		public static void PlayIntroScene() {
			string introScenePath = EditorBuildSettings.scenes[0].path;
			EditorApplication.SaveCurrentSceneIfUserWantsTo();
			EditorApplication.OpenScene(introScenePath);
			EditorApplication.isPlaying = true;
		}
	}
}