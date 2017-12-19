using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	public class EquipProperty : SerializedScriptableObject
	{
		public virtual void initialize(Dictionary<string,string> args) {
		}
		public virtual void onAllyInitAct(GameObject target) {
		}
		public virtual void onEnemyInitAct(GameObject target) {
		}
		public virtual void onUnitCreateAct(GameObject target) {
		}	
		public virtual void onGameStartAct() {
		}
		public virtual void onLevelClearAct() {
		}
		public virtual string getDescription(string args) {
			return "";
		}
	}
}

