using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.LobbyItemSystem {
	public class AddHealth : EquipProperty  {
		[TextArea(0,0)]
		[Header("Health : Mandatory")]
		[Header("ApplyUnitID : Mandatory")]
		public readonly string tip="";

		private string unitID;
		private float addHealth;

		public override void initialize (Dictionary<string,string> args)
		{
			unitID = args ["ApplyUnitID"];
			addHealth = float.Parse(args ["Health"]);
		}
		public override void onUnitCreateAct(GameObject target) {
			BaseUnit unit = target.GetComponent<BaseUnit> ();
			if (unit!=null && unit.uIndex == unitID) {
				unit.maxHealth += addHealth;
				unit.health = unit.maxHealth;
			}
		}
	}
}