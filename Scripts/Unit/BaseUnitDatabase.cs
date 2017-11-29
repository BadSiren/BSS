using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Play;

namespace BSS.Unit {
	public class BaseUnitDatabase : SerializedScriptableObject
	{
		public Dictionary<string,GameObject> unitPrefabs=new Dictionary<string,GameObject>();
		public Dictionary<string,Upgradable> upgrades=new Dictionary<string,Upgradable>();

		public BaseUnit getBaseUnit(string _ID) {
			if (!unitPrefabs.ContainsKey (_ID)) {
				Debug.LogError ("No BaseUnit!");
			}
			return unitPrefabs [_ID].GetComponent<BaseUnit> ();
		}
	}
}

