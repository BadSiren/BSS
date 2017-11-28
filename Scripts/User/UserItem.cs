using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;

namespace BSS {
	[System.Serializable]
	public class UserItem
	{
		public string ID;
		public string itemType;
		public Dictionary<string,Dictionary<string,string>> arguments = new Dictionary<string,Dictionary<string,string>>();

		public UserItem(LobbyItem item) {
			ID=item.ID;
			if (item is LobbyConsumeItem) {
				itemType = "Consume";
			}
			if (item is LobbyEquipItem) {
				itemType = "Equip";
				arguments = (item as LobbyEquipItem).properties;
			}
		}
		public UserItem(string _json) {
			Dictionary<string, string> dic = JsonUtility.FromJson<Serialization<string, string>>(_json).ToDictionary();
			ID = dic ["ID"];
			itemType = dic ["ItemType"];
			dic.Remove ("ID");
			dic.Remove ("ItemType");
			foreach (var it in dic) {
				Dictionary<string, string> args=JsonUtility.FromJson<Serialization<string, string>>(it.Value).ToDictionary();
				arguments [it.Key] = args;
			}
		}
		public string toJson() {
			Dictionary<string,string> dic = new Dictionary<string,string> ();
			dic["ID"]= ID;
			dic["ItemType"]= itemType;
			foreach (var argument in arguments) {
				dic[argument.Key]=JsonUtility.ToJson (new Serialization<string,string>(argument.Value));
			}
			return JsonUtility.ToJson (new Serialization<string,string>(dic));
		}
		public LobbyItem toItem() {
			var item=BSDatabase.instance.lobbyItemDatabase.createLobbyItem<LobbyItem> (ID);
			if (item is LobbyEquipItem) {
				(item as LobbyEquipItem).properties = arguments;
			}
			return item;
		}
	}
}

