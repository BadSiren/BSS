using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyItem : SerializedScriptableObject {
		public string ID;
		public Sprite icon;
		public string itemTitle = "";
		[TextArea()]
		public string itemDescription = "";
		public int buyCost;
		public int sellCost;
		[Range(0,5)]
		public int rairity;
		public Dictionary<string,string> properties=new Dictionary<string,string>();
		public Dictionary<string,string> indexProperties=new Dictionary<string,string>();

		public List<LobbyAct> lobbyActs;

	}



}

