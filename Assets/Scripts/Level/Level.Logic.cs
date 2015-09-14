using UnityEngine;
using System.Collections;

namespace RunAndJump {
	public partial class Level : MonoBehaviour {

		#region SINGLETON
		
		private static Level _instance;
		
		public static Level Instance {
			get {
				if(_instance == null)
				{
					_instance = GameObject.FindObjectOfType<Level>();
				}
				return _instance;
			}
		}
		
		#endregion

		private PlayerController player;
		private LevelCamera playerCamera;
		private Background background;

		private void Awake() {
			_instance = this as Level;
		}

		private void Start () {
			CheckForPlayer ();
			AddCamera ();
			AddBackground ();
			SetGravity ();
			AudioPlayer.Instance.PlayBgm(Bgm);
		}
		
		public void SetGravity () {
			if(Physics2D.gravity.y != Gravity) {
				Physics2D.gravity = new Vector3 (0, Gravity);
			}
		}
		
		private void CheckForPlayer () {
			player = GameObject.FindObjectOfType<PlayerController> ();
			if (player == null) {
				Debug.LogError ("Error: There must to be one CharacterController in the level!");
			}
		}
		
		private void AddCamera () {
			GameObject cameraGO = new GameObject ("PlayerCamera");
			cameraGO.transform.parent = transform;
			playerCamera = cameraGO.AddComponent<LevelCamera> ();
			playerCamera.SetTarget (player.transform);
			playerCamera.Offset = new Vector3 (0, 1, 0);
		}
		
		private void AddBackground () {
			GameObject backgroundGO = new GameObject ("Background");
			backgroundGO.transform.parent = transform;
			background = backgroundGO.AddComponent<Background> ();
			background.Init(Background, playerCamera.Camera);

		}

	}
}
