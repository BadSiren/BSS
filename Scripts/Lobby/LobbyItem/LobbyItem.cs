using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyItem : ScriptableObject {
		public string ID;
		public Sprite icon;
		public string itemTitle = "";
		[TextArea()]
		public string itemDescription = "";
		public int buyCost;
		public int sellCost;
		public int rairity;

		public List<LobbyAct> lobbyActs;
		public List<string> properties;
	}



}

