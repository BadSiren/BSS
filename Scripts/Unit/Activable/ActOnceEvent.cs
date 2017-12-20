using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;


namespace BSS.Unit {
	[System.Serializable]
	public class ActOnceEvent : Activable
	{
		public int useMoney;
		public int useFood;
		public Dictionary<string,int> needUpgrade=new Dictionary<string,int>();

		public UnityEvent actEvent;


		public override void initialize() {}

		public override bool validate() {
			foreach (var it in needUpgrade) {
				if (!GameDataBase.instance.isUpgrade (it.Key, it.Value)) {
					return false;
				}
			}
			return true;
		}

		public override void activate() {
			showInformDynamic ();
			if (!validate()) {
				return;
			}
			if (GameDataBase.instance.useMoneyFood(useMoney,useFood)) {
				actEvent.Invoke ();
			}
		}


		public override string infoContent {
			get {
				return titleContent;
			}
		}	

		protected override void showInformDynamic() {
			var informBoard = (Board.boardList.Find (x => x is InformBoard) as InformBoard);
			if (needUpgrade.Count == 0) {
				informBoard.Show (titleContent, textContent, useMoney , useFood);
			} else if (needUpgrade.Count == 1) {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [0]];
				informBoard.Show (titleContent, textContent, useMoney , useFood,upInfo.icon, needUpgrade [keys [0]]);
			} else {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo0 = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [0]];
				var upInfo1 = BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys [1]];
				informBoard.Show (titleContent, textContent, useMoney , useFood,upInfo0.icon, needUpgrade [keys [0]],upInfo1.icon,needUpgrade[keys[1]]);
			}
		}
	}
}