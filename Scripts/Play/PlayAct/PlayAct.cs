using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BSS.Play {
	public abstract class PlayAct : ScriptableObject  {

		public abstract void onAct (GameObject target,List<string> arguments);
	}
}
