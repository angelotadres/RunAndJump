using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RunAndJump {

	public class LevelSelectScene : BaseScene {
		
		public List<LevelSlot> LevelSlots;

		private void Start() {
			InitLevelSlots ();
		}

		private void InitLevelSlots() {
			for (int i = 0; i < LevelSlots.Count; i++) {
				LevelSlots[i].Init((i < Session.Instance.GetTotalLevels()), i + 1, LevelSlotOnClick);
			}
		}

		private void LevelSlotOnClick(int levelId) {
			Session.Instance.PlayLevel(levelId);
			GoToScene (Scene.LevelHandler);
		}
	}
	
}
