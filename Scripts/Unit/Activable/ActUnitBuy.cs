using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUnitBuy : Activable
	{
		public string unitID;
		public int useMoney;
		public int useFood;
		public Dictionary<string,int> needUpgrade=new Dictionary<string,int>();
		private GameObject unitPrefab;
		private BaseUnit unit;
		private Charactable character;


		public override void initialize() {
			BSDatabase.instance.baseUnitDatabase.unitPrefabs.TryGetValue (unitID,out unitPrefab);
			unit=unitPrefab.GetComponent<BaseUnit> ();
		}

		public override bool validate() {
			if (!GameDataBase.instance.isPopulation (unit.population)) {
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
			showInformDynamic ();
			if (!validate()) {
				return;
			}
			if (GameDataBase.instance.useMoneyFood(useMoney,useFood)) {
				GameObject obj=UnitUtils.CreateUnit (unitPrefab, owner.transform.position+new Vector3(0f,-3f,0f), owner.team);
				BaseEventListener.onPublishGameObject ("UnitBuy", obj);
			}
		}


		public override Sprite icon {
			get {
				return unit.portrait;
			}
		}
		public override string titleContent {
			get {
				return unit.uName.ToString () + " 생산";
			}
		}
		public override string textContent {
			get {
				string replaceText="[" + unit.uName.ToString () + "]";
				replaceText += "\n체력 : " + unit.maxHealth;
				var attackable = unit.GetComponent<Attackable> ();
				if (attackable != null) {
					replaceText += "\n공격력 : " + attackable.initDamage;
					replaceText += "\n공격속도 : " + attackable.initAttackSpeed;
					replaceText += "\n사거리 : " + attackable.initRange;
				}

				return replaceText;
			}
		}
		public override string infoContent {
			get {
				return titleContent;
			}
		}	


		protected override void showInformDynamic() {
			var popInfo=BSDatabase.instance.baseUnitDatabase.upgradeInfos ["MaxPopulation"];
			var informBoard = (Board.boardList.Find (x => x is InformBoard) as InformBoard);
			if (needUpgrade.Count == 0) {
				informBoard.Show (titleContent, textContent, useMoney, useFood,popInfo.icon,unit.population);
			} else if (needUpgrade.Count == 1) {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo=BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys[0]];
				informBoard.Show (titleContent, textContent, useMoney, useFood,popInfo.icon,unit.population,upInfo.icon,needUpgrade[keys[0]]);
			}

		}

	}
}

