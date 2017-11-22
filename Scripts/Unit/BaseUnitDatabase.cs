using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Play;

namespace BSS.Unit {
	public class BaseUnitDatabase : SerializedScriptableObject
	{
		public Dictionary<string,GameObject> unitPrefabs=new Dictionary<string,GameObject>();
		public Dictionary<string,Upgrade> upgrades=new Dictionary<string,Upgrade>();
	}
}

