using UnityEngine;
using System.Collections;

namespace RunAndJump {

	public class HazardSpikesController : LevelPiece {

		public AudioClip PlayerLoseFx;

		private void OnCollisionEnter2D(Collision2D coll) {
			if (coll.gameObject.layer == LayerMask.NameToLayer("Player")) {
				PlayerController player = coll.gameObject.GetComponent<PlayerController>();
				AudioPlayer.Instance.PlaySfx (PlayerLoseFx);
				player.StartPlayerDeath();
			}
		}
	}
}
