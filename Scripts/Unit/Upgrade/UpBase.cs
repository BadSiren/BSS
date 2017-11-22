using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Play;

namespace BSS.Unit {
	public class UpBase : MonoBehaviour
	{
		public string upgradeIndex;
		public int level {
			get {
				if (GameDataBase.instance == null) {
					return 0;
				} 
				return GameDataBase.instance.getUpgradeLevel (upgradeIndex);
			}
			set {
				if (GameDataBase.instance != null) {
					GameDataBase.instance.setUpgradeLevel (upgradeIndex,value);
				} 
			}
		}
		public static List<UpBase> upList=new List<UpBase>();

		protected BaseUnit owner;
		protected ActUpgrade actUpgrade;

		protected virtual void onInitialize() {
			upList.Add (this);
			owner = GetComponent<BaseUnit> ();
		}
		protected virtual void OnDestroy() {
			upList.Remove (this);
		}
		protected virtual void applyUpgrade() {
		}
		public static void allApplyUpgrade(string _index) {
			foreach (var it in upList) {
				if (it.upgradeIndex == _index) {
					it.applyUpgrade ();
				}
			}
		}
	}
}

