using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using BSS.Data;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUnitBuy : Activable
	{
		public string unitIndex;
		public int useMoney;
		public int useFood;
		public bool isInvisible;

		private GameObject buyPrefab;

		public override void onShow() {
			base.onShow ();
			if (buyPrefab == null) {
				buyPrefab = UnitDatabase.instance.unitPrefabDatabaseDic [unitIndex];
				initialize (buyPrefab);
			}
		}
		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			unitBuy (selectUnit.gameObject.transform.position+new Vector3(0f,-1f,0f),selectUnit);

		}

		private void initialize(GameObject prefab) {
			var unit=buyPrefab.GetComponent<BaseUnit> ();
			var character=buyPrefab.GetComponent<Charactable> ();
			var attack=buyPrefab.GetComponent<Attackable> ();
			if (unit != null) {
				titleContent = unit.uName + " 구매하기";
				textContent = unit.uName + "를 구매합니다. \n체력: "+unit.maxHealth.ToString();
				if (attack != null) {
					textContent = textContent + "\n공격력: " + attack.initDamage.ToString ();
				}
			}
			if (character != null) {
				buttonImage = character.portrait;
			}
		}

		private void unitBuy(Vector3 pos,BaseUnit selectUnit) {
			if (buyPrefab==null) {
				//buyUnit is null
				return;
			}
			if (GameDataBase.instance.useMoneyFood(getMoney(),getFood())) {
				GameObject obj=GameObject.Instantiate (buyPrefab, pos, Quaternion.identity);
				obj.SetActive (true);
				BaseUnit unit = obj.GetComponent<BaseUnit> ();
				unit.team = selectUnit.team;
				if (isInvisible) {
					unit.isInvincible = true;
				}
			}
		}

		protected override int getMoney() {
			return useMoney;
		}
		protected override int getFood() {
			return useFood;
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

