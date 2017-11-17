using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.LobbyItemSystem {
	public enum LobbyItemType {
		Equip,Consume
	}

	[System.Serializable]
	public class LobbyItem : ScriptableObject {
		public string ID;
		public LobbyItemType type;
		public Sprite icon;
		public string itemTitle = "";
		public string itemDescription = "";
		public int buyCost;
		public int sellCost;
		public int rairty;

	}



}

