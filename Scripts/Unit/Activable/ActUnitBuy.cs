using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using BSS.Play;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUnitBuy : Activable
	{
		[TextArea(0,0)]
		[Header("NeedTier : Optional")]
		[Header("UseFood : Optional")]
		[Header("UseMoney : Optional")]
		[Header("UnitID : Mandatory")]
		public readonly string tip="";

		private int useMoney;
		private int useFood;
		private Dictionary<string,int> needUpgrade=new Dictionary<string,int>();
		private GameObject unitPrefab;
		private BaseUnit unit;
		private Charactable character;

		public override void initialize(Dictionary<string,string> args) {
			ID = args ["UnitID"];
			BSDatabase.instance.baseUnitDatabase.unitPrefabs.TryGetValue (ID,out unitPrefab);
			unit=unitPrefab.GetComponent<BaseUnit> ();
			icon = unit.portrait;
			titleContent=unit.uName.ToString () + " 생산";

			textContent = "[" + unit.uName.ToString () + "]";
			textContent += "\n체력 : " + unit.maxHealth;
			var attackable = unit.GetComponent<Attackable> ();
			if (attackable != null) {
				textContent += "\n공격력 : " + attackable.initDamage;
				textContent += "\n공격속도 : " + attackable.initAttackSpeed;
				textContent += "\n사거리 : " + attackable.initRange;
			}

			if (args.ContainsKey ("UseMoney")) {
				useMoney = int.Parse (args ["UseMoney"]);
			}
			if (args.ContainsKey ("UseFood")) {
				useFood = int.Parse (args ["UseFood"]);
			}
			foreach (var key in args.Keys) {
				if (key.StartsWith("NeedUp")) {
					needUpgrade.Add(key.Replace("NeedUp",""),int.Parse(args[key]));
				}
			}
		}

			
		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			unitBuy (selectUnit.gameObject.transform.position+new Vector3(0f,-2f,0f),selectUnit);
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
			

		private void unitBuy(Vector3 pos,BaseUnit selectUnit) {
			if (!validate ()) {
				return;
			}
			if (GameDataBase.instance.useMoneyFood(useMoney,useFood)) {
				GameObject obj=UnitUtils.CreateUnit (unitPrefab, pos, selectUnit.team);
				BaseEventListener.onPublishGameObject ("UnitBuy", obj);
			}
		}
		public override string infoContent {
			get {
				return titleContent;
			}
		}

		protected override void showInformDynamic() {
			var popInfo=BSDatabase.instance.baseUnitDatabase.upgradeInfos ["MaxPopulation"];
			if (needUpgrade.Count == 0) {
				UIController.instance.informBoard.Show (titleContent, textContent, useMoney, useFood,popInfo.icon,unit.population);
			} else if (needUpgrade.Count == 1) {
				var keys = new List<string> (needUpgrade.Keys);
				var upInfo=BSDatabase.instance.baseUnitDatabase.upgradeInfos [keys[0]];
				UIController.instance.informBoard.Show (titleContent, textContent, useMoney, useFood,popInfo.icon,unit.population,upInfo.icon,needUpgrade[keys[0]]);
			}

		}

		private string typeToString(string _type) {
			switch (_type) {
			case "Short":
				return "근접 공격";
			case "Long":
				return "원거리 공격";
			}
			return "";
		}

	}
}

