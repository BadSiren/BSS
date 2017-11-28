using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	public class BuyLobbyItem : SerializedMonoBehaviour
	{
		public enum CurrencyType
		{
			Money,Gem
		}
		public CurrencyType type;
		public int needValue;


		public void buyItem(string lobbyItemID) {
			var item=BSDatabase.instance.lobbyItemDatabase.lobbyItems.Find (x => x.ID == lobbyItemID);
			buyItem (item);
		}
		public void buyItem(LobbyItem item) {
			if (item == null) {
				Debug.LogError ("No Item");
				return;
			}
			if (UserJson.instance.getEmptySpace (UserJson.instance.inventoryName) <=0) {
				BaseEventListener.onPublish ("NoSpace");
				return;
			}

			if (type == CurrencyType.Money) {
				if (UserJson.instance.useMoney (needValue)) {
					UserJson.instance.addItem (ScriptableObject.Instantiate (item), UserJson.instance.inventoryName);
				} else {
					BaseEventListener.onPublish ("NoMoney");
				}

			} else if (type == CurrencyType.Gem) {
				if (UserJson.instance.useGem (needValue)) {
					UserJson.instance.addItem (ScriptableObject.Instantiate (item), UserJson.instance.inventoryName);
				} else {
					BaseEventListener.onPublish ("NoGem");
				}
			}
		}

	}
}

