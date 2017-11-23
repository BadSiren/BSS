using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using BSS.Play;

namespace BSS.Unit {
	public abstract class Upgradable : ScriptableObject  {
		public string ID;
		public string titleContent;
		[TextArea()]
		public string textContent;
		public Sprite icon;

		public int level {
			get {
				return GameDataBase.instance.getUpgradeLevel (ID);
			}
		}
		protected GameObject owner;
		

		public abstract void onCreate (GameObject target, float argument);
		public abstract void onUpgradeApply ();
		void OnDestroy() {
			GameDataBase.instance.removeUpListener (this);
		}
	}
}

