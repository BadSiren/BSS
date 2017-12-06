using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Play;
using BSS.UI;



namespace BSS.Unit {
	//UpID : Mandatory

	[System.Serializable]
	public class ActUpgrade: Activable
	{
		[TextArea(0,0)]
		[Header("AddFood : Optional")]
		[Header("InitFood : Optional")]
		[Header("AddMoney : Optional")]
		[Header("InitMoney : Optional")]
		[Header("MaxLevel : Mandatory")]
		[Header("UpID : Mandatory")]
		public readonly string tip="";

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
		public override void initialize(Dictionary<string,string> args) {
			ID = args ["UpID"];
			maxLevel = int.Parse(args ["MaxLevel"]);
			if (args.ContainsKey ("InitMoney")) {
				useInitMoney=int.Parse(args ["InitMoney"]);
			}
			if (args.ContainsKey ("AddMoney")) {
				useAddMoney=int.Parse(args ["AddMoney"]);
			}
			if (args.ContainsKey ("InitFood")) {
				useInitFood=int.Parse(args ["InitFood"]);
			}
			if (args.ContainsKey ("AddFood")) {
				useAddFood=int.Parse(args ["AddFood"]);
			}

			Upgradable up;
			BSDatabase.instance.baseUnitDatabase.upgrades.TryGetValue (ID,out up);
			titleContent = up.titleContent;
			textContent=up.textContent;
			buttonImage = up.icon;
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

