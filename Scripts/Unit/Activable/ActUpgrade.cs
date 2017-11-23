using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Play;
using BSS.UI;



namespace BSS.Unit {
	[System.Serializable]
	public class ActUpgrade: Activable
	{
		public int needProperty=6;

		//Property(+ID)
		public int maxLevel;
		public int useInitMoney;
		public int useAddMoney;
		public int useInitFood;
		public int useAddFood;


		public int level {
			get {
				if (GameDataBase.instance == null) {
					return 0;
				} 
				return GameDataBase.instance.getUpgradeLevel (ID);
			}
			set {
				if (GameDataBase.instance != null) {
					GameDataBase.instance.setUpgradeLevel (ID,value);
				} 
			}
		}


		public override void onInit(string _ID) {
			ID = _ID;
			Upgradable up;
			BSDatabase.instance.baseUnitDatabase.upgrades.TryGetValue (_ID,out up);
			titleContent = up.titleContent;
			textContent=up.textContent;
			buttonImage = up.icon;
		}
		public override void onInit(List<string> properties) {
			if (properties.Count < needProperty) {
				return;
			}
			onInit (properties [0]);
			maxLevel=int.Parse (properties [1]);
			useInitMoney = int.Parse (properties [2]);
			useAddMoney = int.Parse (properties [3]);
			useInitFood = int.Parse (properties [4]);
			useAddFood = int.Parse (properties [5]);
		}

		public override void activate(BaseUnit selectUnit) {
			upgrade ();
			showInformDynamic ();
		}

		private void upgrade() {
			if (level<maxLevel && GameDataBase.instance.useMoneyFood(getMoney(),getFood()) ) {
				level++;
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

