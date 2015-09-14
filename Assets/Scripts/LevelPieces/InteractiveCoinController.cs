using UnityEngine;
using System.Collections;

namespace RunAndJump {

	public class InteractiveCoinController : LevelPiece { 

		public AudioClip CoinPickupFx;
		public delegate void StartInteractionDelegate();
		public static event StartInteractionDelegate StartInteractionEvent;

		private void OnTriggerEnter2D(Collider2D col) {
			if(StartInteractionEvent != null) {
				StartInteractionEvent();
			}
			AudioPlayer.Instance.PlaySfx (CoinPickupFx);
			Destroy (gameObject);
		}

	}

}
