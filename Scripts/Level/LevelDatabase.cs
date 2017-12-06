using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Level {
	public class LevelDatabase : SerializedScriptableObject
	{
		public struct LevelInfo {
			public string title;
			public int maxLevel;
			public int clearMoney;
			public UnitSpawner unitSpawner;
		}
		public Dictionary<string,LevelInfo> levelInfos=new Dictionary<string,LevelInfo>();
	}
}

