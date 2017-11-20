using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using BSS.Play;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUnitChange : Activable
	{
		/*
		public string unitIndex;
		public int useMoney;
		public int useFood;
		public bool isInvisible;

		private GameObject changePrefab;

		public override void onShow() {
			base.onShow ();
			if (changePrefab == null) {
				changePrefab = UnitDatabase.instance.unitPrefabDatabaseDic [unitIndex];
				initialize (changePrefab);
			}
		}

		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			unitChange (selectUnit.gameObject.transform.localPosition+new Vector3(0f,-1f,0f),selectUnit);
		}

		private void initialize(GameObject prefab) {
			var unit=changePrefab.GetComponent<BaseUnit> ();
			var character=changePrefab.GetComponent<Charactable> ();
			var attack=changePrefab.GetComponent<Attackable> ();
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

		private void unitChange(Vector3 pos,BaseUnit selectUnit) {
			if (changePrefab==null) {
				//buyUnit is null
				return;
			}
			if (GameDataBase.instance.useMoneyFood(getMoney(),getFood())) {
				GameObject obj=GameObject.Instantiate (changePrefab, pos, Quaternion.identity);
				obj.SetActive (true);
				BaseUnit unit = obj.GetComponent<BaseUnit> ();
				unit.team = selectUnit.team;
				if (isInvisible) {
					unit.isInvincible = true;
				}
				obj.SendMessage ("onSelect", SendMessageOptions.DontRequireReceiver);
				selectUnit.die ();

			}
		}

		protected int getMoney() {
			return useMoney;
		}
		protected int getFood() {
			return useFood;
		}
		*/

	}
}