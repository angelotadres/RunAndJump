using UnityEngine;
using System;

namespace RunAndJump {
	[Serializable]
	public class LevelMetadata {

		public UnityEngine.Object scene;
		[SerializeField]
		private string levelName;

		public string LevelName {
			get { return (levelName == null || levelName == "") ? "Untitled" : levelName; }
			set { levelName = value; }
		}

		public string SceneName {
			get {
				Debug.Log ("Scene Name : " + scene.name);
				return scene.name;
			}
		}
	}
}