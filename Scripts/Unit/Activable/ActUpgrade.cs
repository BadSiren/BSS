using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Play;
using BSS.UI;



namespace BSS.Unit {
	[System.Serializable]
	public class ActUpgrade: Activable
	{
		[TextArea(0,0)]
		[Header("NeedTier : Optional")]
		[Header("AddFood : Optional")]
		[Header("InitFood : Optional")]
		[Header("AddMoney : Optional")]
		[Header("InitMoney : Optional")]
		[Header("MaxLevel : Mandatory")]
		[Header("UpID : Mandatory")]
		public readonly string tip="";

		private Dictionary<string,int> needUpgrade=new Dictionary<string,int>();
		private int maxLevel;
		private int useInitMoney;
		private int useAddMoney;
		private int useInitFood;
		private int useAddFood;

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
			foreach (var key in args.Keys) {
				if (key.StartsWith("NeedUp")) {
					needUpgrade.Add(key.Replace("NeedUp",""),int.Parse(args[key]));
				}
			}
			ActivableInfo actInfo;
			BSDatabase.instance.baseUnitDatabase.upgradeInfos.TryGetValue (ID,out actInfo);
			titleContent = actInfo.titleContent;
			textContent=actInfo.textContent;
			icon = actInfo.icon;
		}

		public override void activate(BaseUnit selectUnit) {
			upgrade ();
			showInformDynamic ();
		}

		private void upgrade() {
			if (!validate ()) {
				return;
			}
			if (GameDataBase.instance.useMoneyFood(getMoney(),getFood()) ) {
				level++;
			}
		}
		public override bool validate() {
			if (level>=maxLevel) {
				return false;
			}
			foreach (var it in needUpgrade) {
				if (!GameDataBase.instance.isUpgrade (it.Key, it.Value)) {
					return false;
				}
			}
			return true;
		}

		protected override void showInformDynamic() {
			string replace = "레벨 : " + level.ToString () + " / " + maxLevel.ToString () + "\n" + textContent;

			if (needUpgrade.Count == 0) {
				UIController.instance.informBoard.Show (titleContent, replace, getMoney (), getFood ());
			} else if (needUpgrade.Count == 1) {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [0]];
				UIController.instance.informBoard.Show (titleContent, replace, getMoney (), getFood (),upInfo.icon, needUpgrade [keys [0]]);
			} else {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo0 = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [0]];
				var upInfo1 = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [1]];
				UIController.instance.informBoard.Show (titleContent, replace, getMoney (), getFood (),upInfo0.icon, needUpgrade [keys [0]],upInfo1.icon,needUpgrade[keys[1]]);
			}
		}

		protected int getMoney() {
			return useInitMoney+level*useAddMoney;
		}
		protected int getFood() {
			return useInitFood+level*useAddFood;
		}
		public override string infoContent {
			get {
				return titleContent;
			}
		}

	}
}

