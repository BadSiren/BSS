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
		[Header("UseFood : Optional")]
		[Header("UseMoney : Optional")]
		[Header("UnitID : Mandatory")]
		public readonly string tip="";

		private int useMoney;
		private int useFood;
		private GameObject unitPrefab;
		private BaseUnit unit;
		private Charactable character;

		public override void initialize(Dictionary<string,string> args) {
			ID = args ["UnitID"];
			BSDatabase.instance.baseUnitDatabase.unitPrefabs.TryGetValue (ID,out unitPrefab);
			unit=unitPrefab.GetComponent<BaseUnit> ();
			titleContent=unit.uName.ToString () + " 생산하기";
			textContent=unit.uName.ToString () + " 생산합니다.";
			buttonImage = unit.portrait;

			if (args.ContainsKey ("UseMoney")) {
				useMoney = int.Parse (args ["UseMoney"]);
			}
			if (args.ContainsKey ("UseFood")) {
				useFood = int.Parse (args ["UseFood"]);
			}
		}

			
		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			unitBuy (selectUnit.gameObject.transform.position+new Vector3(0f,-2f,0f),selectUnit);
		}
			

		private void unitBuy(Vector3 pos,BaseUnit selectUnit) {
			if (GameDataBase.instance.useMoneyFood(useMoney,useFood)) {
				GameObject obj=GameObject.Instantiate (unitPrefab, pos, Quaternion.identity);
				BaseUnit _unit = obj.GetComponent<BaseUnit> ();
				_unit.team = selectUnit.team;
				BaseEventListener.onPublishGameObject ("UnitBuy", obj);
			}
		}


		protected override void showInformDynamic() {
			UIController.instance.showInform (titleContent,textContent,useMoney,useFood);
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

