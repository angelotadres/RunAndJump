using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace RunAndJump.LevelPackager {
	[CustomEditor(typeof(LevelsPackage))]
	public class LevelsPackageInspector : Editor {

		private LevelsPackage _myTarget;
		private ReorderableList _reorderableList;
		private int _order;
	
		private void OnEnable () {
			_myTarget = target as LevelsPackage;


			if (_myTarget.metadataList == null) {
				_myTarget.metadataList = new List<LevelMetadata> ();
				_myTarget.metadataList.Add (new LevelMetadata ());
			}
			_reorderableList = new ReorderableList (_myTarget.metadataList, typeof(LevelMetadata), true, true, true, true);

			_reorderableList.drawHeaderCallback += DrawHeader;
			_reorderableList.drawElementCallback += DrawElement;
			_reorderableList.onAddCallback += AddItem;
			_reorderableList.onRemoveCallback += RemoveItem;
		}
	
		private void OnDisable () {
			_reorderableList.drawHeaderCallback -= DrawHeader;
			_reorderableList.drawElementCallback -= DrawElement;
			_reorderableList.onAddCallback -= AddItem;
			_reorderableList.onRemoveCallback -= RemoveItem;
		}

		private void DrawHeader (Rect rect) {
			//GUI.Label (new Rect (rect.x + 15, rect.y, rect.width * 0.1f - 15, rect.height), "Order");
			GUI.Label (new Rect (rect.x + 15 + rect.width * 0.11f, rect.y, rect.width * 0.4f - 15, rect.height), "Level Name");
			GUI.Label (new Rect (rect.x + 15 + rect.width * 0.52f, rect.y, rect.width - rect.width * 0.52f - 15, rect.height), "Unity Scene");
		}

		private void DrawElement (Rect rect, int index, bool active, bool focused) {
			LevelMetadata item = _myTarget.metadataList [index];
			EditorGUI.BeginChangeCheck ();
			GUI.Label (new Rect (rect.x, rect.y, rect.width * 0.1f, rect.height), (_order++).ToString());
			item.LevelName = EditorGUI.TextField (new Rect (rect.x + rect.width * 0.11f, rect.y, rect.width * 0.4f, rect.height), item.LevelName);
			item.scene = ValidateLevelScene (EditorGUI.ObjectField (new Rect (rect.x + rect.width * 0.52f, rect.y, rect.width - rect.width * 0.52f, rect.height), item.scene, typeof(Object), false));
			if (EditorGUI.EndChangeCheck ()) {
				EditorUtility.SetDirty (target);
				_myTarget.hasChanges = true;
			}
		}
	
		private void AddItem (ReorderableList list) {
			_myTarget.metadataList.Add (new LevelMetadata ());
			EditorUtility.SetDirty (target);
			_myTarget.hasChanges = true;
		}
	
		private void RemoveItem (ReorderableList list) {
			_myTarget.metadataList.RemoveAt (list.index);
			EditorUtility.SetDirty (target);
			_myTarget.hasChanges = true;
		}
	 
		private Object ValidateLevelScene (Object levelScene) {
			if (levelScene != null) {
				string name = levelScene.ToString ();
				if (name.Contains (" (UnityEngine.SceneAsset)") && name.Contains (LevelsPackage.Suffix)) {
					return levelScene;
				}
			}
			return null;
		}

		public override void OnInspectorGUI () {
			EditorGUILayout.Space ();
			EditorGUILayout.LabelField (string.Format ("{0} Level Packager", Application.productName), EditorStyles.boldLabel);
			_order = 1;
			_reorderableList.DoLayoutList ();

			if (_myTarget.hasChanges) {
				EditorGUILayout.HelpBox ("The list has changes!", MessageType.Warning);
			}
			if (GUILayout.Button ("Commit Levels", GUILayout.Height (30))) {
				EditorUtils.Commit(_myTarget, CommitSuccessCb, CommitErrorCb);
			}
		}

		private void CommitSuccessCb () {
			_myTarget.hasChanges = false;
			EditorUtility.DisplayDialog ("Level List", "Changes were committed with success", "OK");
		}

		private void CommitErrorCb () {
			EditorUtility.DisplayDialog ("Level List", "There was an error doing the commit! Please check the logs.", "OK");
		}
	}
}