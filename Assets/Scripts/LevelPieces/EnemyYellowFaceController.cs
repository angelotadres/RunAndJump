using UnityEngine;
using System.Collections;

namespace RunAndJump {

	public class EnemyYellowFaceController : LevelPiece {

		public AudioClip PlayerLoseFx;

		public Transform GroundCheck;
		public LayerMask WhatIsGround;
		public Transform ObstacleCheck;
		public LayerMask WhatIsObstacle;

		private const float VELOCITY = 2f;
		private const float GROUND_CHECK_RADIUS = 0.02f;
		private const float OBSTACLE_CHECK_RADIUS = 0.02f;

		private bool _facingRight = true;
		private Collider2D _groundCollider;
		private Collider2D _obstacleCollider;
		private Rigidbody2D _groundRigidBody;
		
		private void FixedUpdate () {
			_groundCollider = Physics2D.OverlapCircle (GroundCheck.position, GROUND_CHECK_RADIUS, WhatIsGround);
			_obstacleCollider = Physics2D.OverlapCircle (ObstacleCheck.position, OBSTACLE_CHECK_RADIUS, WhatIsObstacle);
			if (_groundCollider == null || _obstacleCollider != null) {
				Flip ();
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2 (VELOCITY * (_facingRight ? 1: -1), 0);
		}

		private void Flip () {
			_facingRight = !_facingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		private void OnCollisionEnter2D(Collision2D coll) {
			if (coll.gameObject.layer == LayerMask.NameToLayer("Player")) {
				PlayerController player = coll.gameObject.GetComponent<PlayerController>();
				AudioPlayer.Instance.PlaySfx (PlayerLoseFx);
				player.StartPlayerDeath();
			}
		}

	}
}
