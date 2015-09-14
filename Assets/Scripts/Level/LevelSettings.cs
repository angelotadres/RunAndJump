using UnityEngine;
using System;

namespace RunAndJump {

	[Serializable]
	public class LevelSettings : ScriptableObject {

		public float gravity = -30;
		public AudioClip bgm;
		public Sprite background;
	}
}