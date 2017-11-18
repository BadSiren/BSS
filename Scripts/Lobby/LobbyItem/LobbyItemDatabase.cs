using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace BSS.LobbyItemSystem {
	public class LobbyItemDatabase : ScriptableObject
	{
		public static LobbyItemDatabase instance;
		public List<LobbyItem> lobbyItems=new List<LobbyItem>();

		void OnEnable() {
			if (instance == null) {
				instance = this;
			}
		}
			

		public LobbyItem getItem(string _ID){
			LobbyItem item=lobbyItems.Find(x=>x.ID==_ID);
			return item;
		}

	}
}

