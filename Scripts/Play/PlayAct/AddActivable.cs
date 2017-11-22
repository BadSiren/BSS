using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.Play {
	public class AddActivable : PlayAct  {
		public override void onAct (GameObject target,List<string> arguments)
		{
			string actId = arguments [0];
			Activable temp=BSDatabase.instance.activableDatabase.activables [actId];
			Activable act=ScriptableObject.Instantiate (temp);

			List<string> _arguments = new List<string> ();
			for (int i=1;i<arguments.Count; i++) {
				_arguments.Add (arguments[i]);
			}
			act.onInit (_arguments);

			target.SendMessage ("addActivable", act, SendMessageOptions.DontRequireReceiver);
		}
	}
}

