using UnityEngine;
using UnityEditor;

namespace RunAndJump.LevelPackager {
	public static class MenuItems {
		
		[MenuItem ("Tools/Level Packager/Show Levels Package")]
		private static void ShowLevelList () {
			LevelsPackage data = Resources.Load (LevelsPackage.ResourcePath) as LevelsPackage;
			if (data == null) {
				data = ScriptableObject.CreateInstance<LevelsPackage> ();
				AssetDatabase.CreateAsset (data, LevelsPackage.FullPath);
				AssetDatabase.Refresh ();
				AssetDatabase.SaveAssets ();
				
			}
			Selection.activeObject = data;
		}

	}        
}