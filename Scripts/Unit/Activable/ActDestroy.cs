using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	[System.Serializable]
	public class ActDestroy : Activable
	{
		[TextArea(0,0)]
		public readonly string tip="";

		public override void initialize(Dictionary<string,string> args) {
		}


		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			selectUnit.die ();
		}

	}
}

