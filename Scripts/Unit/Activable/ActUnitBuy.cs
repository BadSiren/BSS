using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using BSS.Play;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUnitBuy : Activable
	{
		public int needProperty=5;

		//Property
		public int useMoney;
		public int useFood;
		public float addHealth;
		public float addDamage;

		private GameObject unitPrefab;
		private BaseUnit unit;
		private Charactable character;

		public override void onInit(string _ID) {
			ID = _ID;
			BSDatabase.instance.baseUnitDatabase.unitPrefabs.TryGetValue (_ID,out unitPrefab);
			unit=unitPrefab.GetComponent<BaseUnit> ();
			titleContent=unit.uName.ToString () + " 생산하기";
			textContent=unit.uName.ToString () + " 생산합니다.";
			character = unitPrefab.GetComponent<Charactable> ();
			if (character != null) {
				buttonImage = character.portrait;
			}
		}
		public override void onInit(List<string> properties) {
			if (properties.Count < needProperty) {
				return;
			}
			onInit (properties [0]);
			useMoney = int.Parse (properties [1]);
			useFood = int.Parse (properties [2]);
			addHealth = int.Parse (properties [3]);
			addDamage = int.Parse (properties [4]);
		}
			
		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			unitBuy (selectUnit.gameObject.transform.position+new Vector3(0f,-1f,0f),selectUnit);
		}
			

		private void unitBuy(Vector3 pos,BaseUnit selectUnit) {
			if (GameDataBase.instance.useMoneyFood(useMoney,useFood)) {
				GameObject obj=GameObject.Instantiate (unitPrefab, pos, Quaternion.identity);
				BaseUnit _unit = obj.GetComponent<BaseUnit> ();
				_unit.team = selectUnit.team;
				_unit.maxHealth += addHealth;
				_unit.health = _unit.maxHealth;
				Attackable _attack = obj.GetComponent<Attackable> ();
				if (_attack != null) {
					_attack.changeDamage += addDamage;
				}

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

