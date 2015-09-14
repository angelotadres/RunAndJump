using UnityEngine;
using System.Collections;

namespace RunAndJump {
	
	public class InteractiveTreasureController : LevelPiece { 

		public AudioClip TreasurePickupFx;
		public delegate void StartInteractionDelegate();
		public static event StartInteractionDelegate StartInteractionEvent;
		
		private void OnTriggerEnter2D(Collider2D col) {
			if(StartInteractionEvent != null) {
				StartInteractionEvent();
			}
			AudioPlayer.Instance.PlaySfx (TreasurePickupFx);
			Destroy (gameObject);
		}
		
	}
	
}
