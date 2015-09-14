using UnityEngine;
using System.Collections.Generic;

namespace RunAndJump {
	public class Background : MonoBehaviour {

		private Sprite _bg;
		private Camera _boundsCam;
		private Vector3 _camPos;
		private Vector3 _lastCamPos;
		private List<Transform> _bgTransforms;
		private List<SpriteRenderer> _bgRenderers;
		private bool _ready;

		private int _totalX = 3;
		private int _totalY = 1;

		private Vector3 _delta;

		public void Init(Sprite bg, Camera boundsCam) {
			if(bg == null) {
				Debug.LogErrorFormat(this, "There isn't a sprite asociated!");
				return;
			}
			_bg = bg;
			_boundsCam = boundsCam;
			_camPos = boundsCam.transform.position;
			_lastCamPos = _camPos;
			_delta = Vector3.zero;
			CreateInstances();
			_ready = true;
		}
		 
		private void CreateInstances () {
			_bgTransforms = new List<Transform>();
			_bgRenderers = new List<SpriteRenderer>();
			for (int i = 0 ; i < _totalX ; i++) {
				for (int j = 0 ; j < _totalY ; j++) {
					CreateInstance(i, j);
				}
			}
			// FIXME: QUICK FIX
			gameObject.transform.parent = _boundsCam.transform;
			gameObject.transform.position += new Vector3(0, -5, 20);

		}

		private void CreateInstance(int xPos, int yPos) {
			GameObject go = new GameObject ("bg");
			SpriteRenderer sp = go.AddComponent<SpriteRenderer> ();
			sp.sprite = _bg;
			sp.sortingOrder = -1;
			go.transform.parent = transform;
			go.transform.position = _boundsCam.transform.position + new Vector3 ((_bg.bounds.extents.x * 2) * (xPos - _totalX / 2), (_bg.bounds.extents.y * 2) * (yPos - _totalY / 2), 0);
			_bgTransforms.Add (go.transform);
			_bgRenderers.Add(sp);
		}
	}
}
