using UnityEngine;
using System.Collections;
using BSS.Data;
using BSS.UI;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUpgrade: Activable
	{
		public string upgradeIndex;
		public int useInitMoney;
		public int useAddMoney;
		public int useInitFood;
		public int useAddFood;

		public int maxLevel;
		public int level {
			get {
				return GameDataBase.instance.getUpgrade (upgradeIndex);
			}
		}

		public override void activate(BaseUnit selectUnit) {
			upgrade ();
			showInformDynamic ();
		}

		private void upgrade() {
			if (level<maxLevel && GameDataBase.instance.useMoneyFood(getMoney(),getFood()) ) {
				GameDataBase.instance.SendMessage ("increaseUpgrade" ,upgradeIndex, SendMessageOptions.DontRequireReceiver);
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

