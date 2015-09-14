using UnityEngine;
using System.Collections;

namespace RunAndJump {
	public class Singleton<T> : MonoBehaviour where T: class {

		private static T instance = null;

		public static T Instance {
			get {
				if (instance == null)
					instance = SingletonManager.Go.AddComponent (typeof(T)) as T;
				return instance;
			}
		}
		
		public static void Instantiate () {
			instance = Instance;
		}
		
		public Singleton () {
			instance = this as T;
		}
	}

	public class SingletonManager {

		private static GameObject go = null;

		public static GameObject Go {
			get {
				if (go == null) {
					go = new GameObject ("Singleton");
					Object.DontDestroyOnLoad (go);
				}
				return go;
			}
		}
	}
}