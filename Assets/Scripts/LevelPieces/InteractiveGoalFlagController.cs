using UnityEngine;
using System.Collections;

namespace RunAndJump {
	
	public class InteractiveGoalFlagController : LevelPiece { 

		public AudioClip PlayerWinFx;
		
		public delegate void StartInteractionDelegate();
		public static event StartInteractionDelegate StartInteractionEvent;
		
		private void OnTriggerEnter2D(Collider2D col) {
			if(StartInteractionEvent != null) {
				AudioPlayer.Instance.PlaySfx (PlayerWinFx);
				StartInteractionEvent();
			}
		}
		
	}
	
}
