using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using BSS.Data;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUnitChange : Activable
	{
		public GameObject changePrefab;
		public int useMoney;
		public int useFood;
		public bool isInvisible;

		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			unitChange (selectUnit.gameObject.transform.localPosition+new Vector3(0f,-1f,0f),selectUnit);
		}

		private void unitChange(Vector3 pos,BaseUnit selectUnit) {
			if (changePrefab==null) {
				//buyUnit is null
				return;
			}
			if (GameDataBase.instance.useMoneyFood(getMoney(),getFood())) {
				GameObject obj=GameObject.Instantiate (changePrefab, pos, Quaternion.identity);
				BaseUnit unit = obj.GetComponent<BaseUnit> ();
				unit.team = selectUnit.team;
				if (isInvisible) {
					unit.isInvincible = true;
				}
				obj.SendMessage ("onSelect", SendMessageOptions.DontRequireReceiver);
				selectUnit.die ();

			}
		}

		protected override int getMoney() {
			return useMoney;
		}
		protected override int getFood() {
			return useFood;
		}

	}
}