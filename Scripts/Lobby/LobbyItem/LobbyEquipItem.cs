using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyEquipItem : LobbyItem
	{
		public class Property  {
			public string target;
			public string playAct;
			public List<string> arguments=new List<string> ();
		}
		public string containerName;
		public List<Property> properties=new List<Property>();

	}
}

