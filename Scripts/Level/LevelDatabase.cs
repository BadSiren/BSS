using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Level {
	public class LevelDatabase : SerializedScriptableObject
	{
		
		public Dictionary<int,string> difficulties=new Dictionary<int,string>();
		public Dictionary<int,UnitSpawner> unitSpawners=new Dictionary<int,UnitSpawner>();
	}
}

