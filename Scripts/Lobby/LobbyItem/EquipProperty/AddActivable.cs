using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.LobbyItemSystem {
	public class AddActivable : EquipProperty  {
		/*
		[TextArea()]
		[Header("ActID : Mandatory")]
		[Header("ApplyUnitID : Mandatory")]
		public string description="";

		private Activable activable;
		private string unitID;

		public override void initialize (Dictionary<string,string> args)
		{
			Activable temp=BSDatabase.instance.activableDatabase.activables [args ["ActID"]];
			unitID = args ["ApplyUnitID"];
			activable=ScriptableObject.Instantiate (temp);
			activable.initialize (args);
		}
		public override void onUnitCreateAct(GameObject target) {
			BaseUnit unit = target.GetComponent<BaseUnit> ();
			if (unit!=null && unit.uIndex == unitID) {
				target.SendMessage ("addActivable", activable, SendMessageOptions.DontRequireReceiver);
			}
		}
		public override string getDescription(Dictionary<string,string> args) {
			return description;
		}
		*/

	}
}

