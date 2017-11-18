using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using BSS.Data;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUnitBuy : Activable
	{
		public GameObject buyPrefab;
		public int useMoney;
		public int useFood;
		public bool isInvisible;

		private BaseUnit unit;
		private Charactable character;
		private Attackable attack;

		void OnEnable() {
			unit=buyPrefab.GetComponent<BaseUnit> ();
			if (unit == null) {
				Debug.LogError ("Not Unit!");
			}
			character=buyPrefab.GetComponent<Charactable> ();
			attack=buyPrefab.GetComponent<Attackable> ();
		}

		public override void onShow() {
			base.onShow ();
		}
		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			unitBuy (selectUnit.gameObject.transform.position+new Vector3(0f,-1f,0f),selectUnit);
		}
			

		private void unitBuy(Vector3 pos,BaseUnit selectUnit) {
			if (buyPrefab==null) {
				//buyUnit is null
				return;
			}
			if (GameDataBase.instance.useMoneyFood(useMoney,useFood)) {
				GameObject obj=GameObject.Instantiate (buyPrefab, pos, Quaternion.identity);
				obj.SetActive (true);
				BaseUnit unit = obj.GetComponent<BaseUnit> ();
				unit.team = selectUnit.team;
				unit.isInvincible = isInvisible;
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

