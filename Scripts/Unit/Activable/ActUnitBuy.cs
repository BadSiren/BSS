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

		public override void onShow() {
			base.onShow ();
			if (buttonImage == null) {
				var character=buyPrefab.GetComponent<Charactable> ();
				if (character == null) {
					return;
				}
				buttonImage = character.portrait;
			}
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
			if (GameDataBase.instance.useMoneyFood(getMoney(),getFood())) {
				GameObject obj=GameObject.Instantiate (buyPrefab, pos, Quaternion.identity);
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

	}
}

