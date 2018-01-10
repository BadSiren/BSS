using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUpgrade: Activable
	{
		public string upID;
		public int maxLevel;
		public int useInitMoney;
		public int useAddMoney;
		public int useInitFood;
		public int useAddFood;
		public Dictionary<string,int> needUpgrade=new Dictionary<string,int>();
		public bool isPublishInt=false;
		[ShowIf("isPublishInt")]
		public string publishName="";

		private ActivableInfo upInfo;


		public int level {
			get {
				if (GameDataBase.instance == null) {
					return 0;
				} 
				return GameDataBase.instance.getUpgradeLevel (upID);
			}
			set {
				if (GameDataBase.instance != null) {
					GameDataBase.instance.setUpgradeLevel (upID,value);
				} 
			}
		}

		public override void initialize() {
			BSDatabase.instance.baseUnitDatabase.upgradeInfos.TryGetValue (upID,out upInfo);
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

		public override void activate() {
			if (!validate ()) {
				return;
			}
			if (GameDataBase.instance.useMoneyFood(needMoney(),needFood()) ) {
				level++;
				if (isPublishInt) {
					BaseEventListener.onPublishInt (publishName, level);
				}
			}

			showInformDynamic ();
		}

		public override Sprite icon {
			get {
				return upInfo.icon;
			}
		}
		public override string titleContent {
			get {
				return upInfo.titleContent;
			}
		}
		public override string textContent {
			get {
				return upInfo.textContent;
			}
		}
		public override string infoContent {
			get {
				return titleContent;
			}
		}



		protected override void showInformDynamic() {
			string replace = "레벨 : " + level.ToString () + " / " + maxLevel.ToString () + "\n" + textContent;

			var informBoard = (Board.boardList.Find (x => x is InformBoard) as InformBoard);
			if (needUpgrade.Count == 0) {
				informBoard.Show (titleContent, replace, needMoney (), needFood ());
			} else if (needUpgrade.Count == 1) {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [0]];
				informBoard.Show (titleContent, replace, needMoney (), needFood (),upInfo.icon, needUpgrade [keys [0]]);
			} else {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo0 = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [0]];
				var upInfo1 = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [1]];
				informBoard.Show (titleContent, replace, needMoney (), needFood (),upInfo0.icon, needUpgrade [keys [0]],upInfo1.icon,needUpgrade[keys[1]]);
			}
		}

		private int needMoney() {
			return useInitMoney+level*useAddMoney;
		}
		private int needFood() {
			return useInitFood+level*useAddFood;
		}

	}
}

