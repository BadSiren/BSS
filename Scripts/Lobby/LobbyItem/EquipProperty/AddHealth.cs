using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.LobbyItemSystem {
	public class AddHealth : EquipProperty  {
		[TextArea()]
		[Header("Health : Mandatory")]
		[Header("ApplyUnitID : Mandatory")]
		public string description="";

		private string unitID;
		private float addHealth;

		public override void initialize (Dictionary<string,string> args)
		{
			unitID = args ["ApplyUnitID"];
			addHealth = float.Parse(args ["Health"]);
		}
		public override void onUnitCreateAct(GameObject target) {
			BaseUnit unit = target.GetComponent<BaseUnit> ();
			if (unit!=null && unit.uIndex == unitID ) {
				unit.maxHealth += addHealth;
				unit.health = unit.maxHealth;
			}
		}
		public override void onSetEnemyAct(GameObject target) {
			BaseUnit unit = target.GetComponent<BaseUnit> ();
			if (unit!=null && unit.uIndex == unitID ) {
				unit.maxHealth -= addHealth;
				unit.health -= addHealth;
			}
		}
		public override string getDescription(Dictionary<string,string> args) {
			string revise = description;
			revise=revise.Replace ("@Health", args["Health"]);
			revise=revise.Replace ("@ApplyUnitID", getUnitName(args["ApplyUnitID"]));
			return revise;
		}
		private string getUnitName(string _ID) {
			var unit =BSDatabase.instance.baseUnitDatabase.getBaseUnit (_ID);
			return unit.uName;
		}
	}
}