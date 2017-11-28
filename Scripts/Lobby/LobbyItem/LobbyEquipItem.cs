using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyEquipItem : LobbyItem
	{
		public string containerName;
		public Dictionary<string,Dictionary<string,string>> properties=new Dictionary<string,Dictionary<string,string>>();

	}
}

