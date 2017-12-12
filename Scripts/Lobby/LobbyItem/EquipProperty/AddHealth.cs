using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.LobbyItemSystem {
	public class AddHealth : EquipProperty  {
		[TextArea()]
		[Header("PlainHealth : Optional")]
		[Header("ApplyUnitID : Mandatory")]
		public string description="";

		private string unitID;
		private float addPlainHealth;

		public override void initialize (Dictionary<string,string> args)
		{
			unitID = args ["ApplyUnitID"];
			if (args.ContainsKey ("PlainHealth")) {
				addPlainHealth = int.Parse (args ["PlainHealth"]);
			}
		}
		public override void onAllyInitAct(GameObject target) {
			BaseUnit unit = target.GetComponent<BaseUnit> ();
			if (unit!=null && unit.uIndex == unitID ) {
				unit.maxHealth += addPlainHealth;
				unit.health = unit.maxHealth;
			}
		}
		public override string getDescription(Dictionary<string,string> args) {
			string revise = description;
			revise=revise.Replace ("@PlainHealth", args["PlainHealth"]);
			revise=revise.Replace ("@ApplyUnitID", getUnitName(args["ApplyUnitID"]));
			return revise;
		}
		private string getUnitName(string _ID) {
			var unit =BSDatabase.instance.baseUnitDatabase.getBaseUnit (_ID);
			return unit.uName;
		}
	}
}