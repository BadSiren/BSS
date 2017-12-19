using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.LobbyItemSystem {
	public class AddDamage : EquipProperty  {
		/*
		[TextArea()]
		[Header("PlainDamage : Optional")]
		[Header("ApplyUnitID : Mandatory")]
		public string description="";

		private string unitID;
		private float addPlainDamage;

		public override void initialize (Dictionary<string,string> args)
		{
			unitID = args ["ApplyUnitID"];
			if (args.ContainsKey ("PlainDamage")) {
				addPlainDamage = int.Parse (args ["PlainDamage"]);
			}
		}
		public override void onAllyInitAct(GameObject target) {
			BaseUnit unit = target.GetComponent<BaseUnit> ();
			if (unit!=null && unit.uIndex == unitID ) {
				var attackable = unit.GetComponent<Attackable> ();
				if (attackable != null) {
					attackable.changeDamage += addPlainDamage;
				}
			}
		}
		public override string getDescription(Dictionary<string,string> args) {
			string revise = description;
			if (args.ContainsKey ("PlainDamage")) {
				revise = revise.Replace ("@PlainDamage", args ["PlainDamage"]);
			}
			revise=revise.Replace ("@ApplyUnitID", getUnitName(args["ApplyUnitID"]));
			return revise;
		}
		private string getUnitName(string _ID) {
			var unit =BSDatabase.instance.baseUnitDatabase.getBaseUnit (_ID);
			return unit.uName;
		}
		*/
	}
}

