using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.LobbyItemSystem {
	public class LobbyItemDatabase : ScriptableObject
	{
		public List<LobbyItem> lobbyItems = new List<LobbyItem> ();

		private List<LobbyEquipItem> equipItems = new List<LobbyEquipItem> ();
		private List<LobbyConsumeItem> consumeItems = new List<LobbyConsumeItem> ();

		public List<LobbyEquipItem> getLobbyEquipItem() {
			if (equipItems.Count > 0) {
				return equipItems;
			}
			var items=lobbyItems.FindAll (x => x is LobbyEquipItem);
			foreach (var it in items) {
				equipItems.Add (it as LobbyEquipItem);
			}
			return equipItems;
		}
		public List<LobbyConsumeItem> getLobbyConsumeItem() {
			if (consumeItems.Count > 0) {
				return consumeItems;
			}
			var items=lobbyItems.FindAll (x => x is LobbyConsumeItem);
			foreach (var it in items) {
				consumeItems.Add (it as LobbyConsumeItem);
			}
			return consumeItems;
		}
	}
}

