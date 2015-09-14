using UnityEngine;
using UnityEditor;
//using System.Linq;
using System.Collections.Generic;

namespace RunAndJump.LevelPackager {
	public static class EditorUtils {

		public static void Commit (LevelsPackage package, System.Action successCb = null, System.Action errorCb = null) {
			try {
				List<EditorBuildSettingsScene> buildScenes = new List<EditorBuildSettingsScene> (EditorBuildSettings.scenes);
				List<EditorBuildSettingsScene> levelScenes = new List<EditorBuildSettingsScene> ();
				List<EditorBuildSettingsScene> buildScenesToRemove = new List<EditorBuildSettingsScene> ();
				List<LevelMetadata> levelListmetadatasToRemove = new List<LevelMetadata> ();
				
				// Check level scenes in original Build Scenes
				foreach (EditorBuildSettingsScene scene in buildScenes) {
					if (scene.path.Contains (LevelsPackage.Suffix)) {
						buildScenesToRemove.Add (scene);
					}
				}
				// Remove level scenes
				foreach (EditorBuildSettingsScene scene in buildScenesToRemove) {
					buildScenes.Remove (scene);
				}
				// Create Scenes references based on LevelMatadata, and in the process keep track of the ones with null value
				foreach (LevelMetadata metadata in package.metadataList) {
					if (metadata.scene != null) {
						string pathToScene = AssetDatabase.GetAssetPath (metadata.scene);
						EditorBuildSettingsScene scene = new EditorBuildSettingsScene (pathToScene, true);
						levelScenes.Add (scene);
					} else {
						levelListmetadatasToRemove.Add (metadata);
					}
				}
				// Removing null value metadatas from levelList
				foreach (LevelMetadata metadata in levelListmetadatasToRemove) {
					package.metadataList.Remove (metadata);
				}
				// Removing duplicated from levelScenes
				// levelScenes = levelScenes.Distinct().ToList<EditorBuildSettingsScene>();
				// Comminting changes
				buildScenes.AddRange (levelScenes);
				EditorBuildSettings.scenes = buildScenes.ToArray ();
				if (successCb != null) {
					successCb ();
				}
			} catch (UnityException) {
				if (errorCb != null) {
					errorCb ();
				}
			}
		}

	}
}