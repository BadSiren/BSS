using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Play;

namespace BSS.LobbyItemSystem {
	public class LobbyItemDatabase : SerializedScriptableObject
	{
		public struct RairityInfo {
			public string title;
			public Color col;
		}

		public List<LobbyItem> lobbyItems = new List<LobbyItem> ();
		public Dictionary<int,RairityInfo> rairityInfos=new Dictionary<int,RairityInfo>();
		public Dictionary<string,PlayAct> playActs=new Dictionary<string,PlayAct>();

		public List<LobbyEquipItem> getLobbyEquipItems() {
			List<LobbyEquipItem> equipItems = new List<LobbyEquipItem> ();
			var items=lobbyItems.FindAll (x => x is LobbyEquipItem);
			foreach (var it in items) {
				equipItems.Add (it as LobbyEquipItem);
			}
			return equipItems;
		}
		public List<LobbyConsumeItem> getLobbyConsumeItems() {
			List<LobbyConsumeItem> consumeItems = new List<LobbyConsumeItem> ();
			var items=lobbyItems.FindAll (x => x is LobbyConsumeItem);
			foreach (var it in items) {
				consumeItems.Add (it as LobbyConsumeItem);
			}
			return consumeItems;
		}
	}
}

