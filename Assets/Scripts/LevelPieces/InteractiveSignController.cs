using UnityEngine;
using System.Collections;

namespace RunAndJump {

	public class InteractiveSignController : LevelPiece {

		public AudioClip SignFx;
		public string Message;

		public delegate void StartInteractionDelegate(string message);
		public static event StartInteractionDelegate StartInteractionEvent;

		public delegate void StopInteractionDelegate();
		public static event StopInteractionDelegate StopInteractionEvent;

		private void OnTriggerEnter2D(Collider2D col) {
			if(StartInteractionEvent != null) {
				AudioPlayer.Instance.PlaySfx (SignFx);
				StartInteractionEvent(Message);
			}
		}

		private void OnTriggerExit2D(Collider2D col) {
			if(StopInteractionEvent != null) {
				StopInteractionEvent();
			}

		}
	}
}
