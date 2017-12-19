using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;

namespace BSS {
	[System.Serializable]
	public class UserItem
	{
		public string ID;
		//Index
		public Dictionary<string,string> indexProperties = new Dictionary<string,string>();

		public UserItem(LobbyItem item) {
			ID=item.ID;
			foreach (var indexProperty in item.indexProperties) {
				indexProperties [indexProperty.Key] = indexProperty.Value;
			}
		}
		public UserItem(string _json) {
			Dictionary<string, string> dic = JsonUtility.FromJson<Serialization<string, string>>(_json).ToDictionary();
			ID = dic ["ID"];
			dic.Remove ("ID");

			indexProperties.Clear ();
			if (dic.ContainsKey ("IndexProperties")) {
				var _indexProperties = JsonUtility.FromJson<Serialization<string, string>> (dic ["IndexProperties"]).ToDictionary ();
				foreach (var indexProperty in _indexProperties) {
					indexProperties [indexProperty.Key] = indexProperty.Value;
				}
			}
		}

		public string toJson() {
			Dictionary<string,string> dic = new Dictionary<string,string> ();
			dic["ID"]= ID;
			dic["IndexProperties"] = JsonUtility.ToJson (new Serialization<string,string> (indexProperties));

			return JsonUtility.ToJson (new Serialization<string,string>(dic));
		}
			
		public LobbyItem toItem() {
			var item=BSDatabase.instance.lobbyItemDatabase.createLobbyItem<LobbyItem> (ID,indexProperties);
			return item;
		}
	}
}

