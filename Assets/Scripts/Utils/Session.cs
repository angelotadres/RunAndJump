using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RunAndJump {

	public class Session : Singleton<Session>  {

		private LevelsPackage _levels;
		private int _currentLevelId = 0;

		private void Awake() {
			_levels = Resources.Load<LevelsPackage>(LevelsPackage.ResourcePath);
		}

		public void PlayLevel(int id) {
			_currentLevelId = id;
		}

		public void PlayNext() {
			_currentLevelId++;
		}

		public LevelMetadata GetLevelMetadata() {
			return _levels.metadataList[_currentLevelId -1];
		}

		public bool HasNext() {
			return (_currentLevelId < _levels.metadataList.Count);
		}

		public int GetLevelId() {
			return _currentLevelId;
		}

		public int GetTotalLevels() {
			return _levels.metadataList.Count;
		}
	}

}
