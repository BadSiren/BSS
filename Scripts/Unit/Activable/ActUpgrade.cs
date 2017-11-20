using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Play;
using BSS.UI;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUpgrade: Activable
	{
		public static List<ActUpgrade> actUpgradeList=new List<ActUpgrade>();
		public string upgradeIndex;
		public int useInitMoney;
		public int useAddMoney;
		public int useInitFood;
		public int useAddFood;

		public int maxLevel;
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

		void OnEnable() {
			if (!actUpgradeList.Contains (this)) {
				actUpgradeList.Add (this);
			}
		}
		void OnDisable() {
			if (actUpgradeList.Contains (this)) {
				actUpgradeList.Remove (this);
			}
		}

		public override void activate(BaseUnit selectUnit) {
			upgrade ();
			showInformDynamic ();
		}

		public static ActUpgrade getActUpgrade(string upIndex) {
			return actUpgradeList.Find (x => x.upgradeIndex == upIndex);
		}

		private void upgrade() {
			if (level<maxLevel && GameDataBase.instance.useMoneyFood(getMoney(),getFood()) ) {
				level++;
				UpBase.allApplyUpgrade (upgradeIndex);
			}
		}

		protected override void showInformDynamic() {
			string temp = "레벨 : " + level.ToString () + " / " + maxLevel.ToString () + "\n" + textContent;
			UIController.instance.showInform (titleContent,temp,getMoney(),getFood());
		}

		protected int getMoney() {
			return useInitMoney+level*useAddMoney;
		}
		protected int getFood() {
			return useInitFood+level*useAddFood;
		}

	}
}

