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
		public int rairity;

		public List<LobbyAct> lobbyActs;
	}



}

