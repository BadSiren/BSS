using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS.Unit {
	public class UpMoney : Upgradable {
		[Header("UpMoney")]
		public bool isMultipleLevel=true;
		public int addMoney;
		public int addFood;

		public override void applyUpgrade(string _ID){
			if (ID != _ID) {
				return;
			}
			if (isMultipleLevel) {
				GameDataBase.instance.money += level * addMoney;
				GameDataBase.instance.food += level * addFood;
			} else {
				GameDataBase.instance.money += addMoney;
				GameDataBase.instance.food += addFood;
			}

		}
	}
}

