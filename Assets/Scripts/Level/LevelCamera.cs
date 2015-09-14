using UnityEngine;
using System.Collections;

namespace RunAndJump {
	[RequireComponent(typeof(Camera))]
	public class LevelCamera : MonoBehaviour {
		
		public Transform Target;
		public Vector3 Offset;
		public Rect _levelBounds;

		private Rect _screenExtents;
		private Camera _camera;

		public Camera Camera {
			get {
				return _camera;
			}
		}
		[SerializeField]
		private bool _useBoundRestrictions = false;
		private float depth = -1;

		public void Awake() {
			//camera.orthographicSize = (Screen.height / 100f / 2.0f);
			_camera = GetComponent<Camera>();
			_camera.orthographic = true;
			_camera.orthographicSize = 4;
			_screenExtents = new Rect(-_camera.orthographicSize * _camera.aspect, -_camera.orthographicSize, _camera.aspect * _camera.orthographicSize * 2, _camera.orthographicSize * 2 );
		}

		// Set target
		public void SetTarget(Transform t) {
			Target = t;
			transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z) + Offset;
		}

		private void LateUpdate() {
			if (Target) {
				if(_useBoundRestrictions) {
					transform.position = ApplyBoundRestrictions(new Vector3(Target.position.x, Target.position.y, depth) + Offset);
				} else {
					transform.position = new Vector3(Target.position.x, Target.position.y, depth) + Offset;
				}
			}
		}

		private Vector3 ApplyBoundRestrictions(Vector3 position) {
			float _boundOffset;
			
			// Check Right
			_boundOffset = (position.x + _screenExtents.max.x) - _levelBounds.max.x;
			if(_boundOffset > 0) {
				position = new Vector3(position.x - _boundOffset , position.y, position.z);
			}
			// Check Left
			_boundOffset = (position.x + _screenExtents.min.x) - _levelBounds.min.x;
			if(_boundOffset < 0) {
				position = new Vector3(position.x - _boundOffset, position.y, position.z);
			}
			// Check Top
			_boundOffset = (position.y + _screenExtents.max.y) - _levelBounds.max.y;
			if(_boundOffset > 0) {
				position = new Vector3(position.x, position.y - _boundOffset, position.z);
			}
			// Check Bottom
			_boundOffset = (position.y + _screenExtents.min.y) - _levelBounds.min.y;
			if(_boundOffset < 0) {
				position = new Vector3(position.x, position.y - _boundOffset, position.z);
			}
			return position;
		}

		private void OnDrawGizmos () {
			/*CH02*/
			Gizmos.DrawLine (new Vector3 (_levelBounds.x, _levelBounds.y, 0), new Vector3 (_levelBounds.x + _levelBounds.width, _levelBounds.y, 0));
			Gizmos.DrawLine (new Vector3 (_levelBounds.x + _levelBounds.width, _levelBounds.y, 0), new Vector3 (_levelBounds.x + _levelBounds.width, _levelBounds.y + _levelBounds.height, 0));
			Gizmos.DrawLine (new Vector3 (_levelBounds.x + _levelBounds.width, _levelBounds.y + _levelBounds.height, 0), new Vector3 (_levelBounds.x, _levelBounds.y + _levelBounds.height, 0));
			Gizmos.DrawLine (new Vector3 (_levelBounds.x, _levelBounds.y + _levelBounds.height, 0), new Vector3 (_levelBounds.x, _levelBounds.y, 0));
			/*CH02*/
		}
	}
}