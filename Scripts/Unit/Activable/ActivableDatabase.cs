using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public class ActivableDatabase : SerializedScriptableObject
	{
		public Dictionary<string,Activable> activables=new Dictionary<string,Activable>();
	}
}

