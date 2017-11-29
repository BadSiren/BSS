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

		public static LobbyItem lastBuyItem;
		public static List<LobbyItem> lastBuyItems=new List<LobbyItem> ();

		public void buyItem(string lobbyItemID) {
			if (!buyValidate(1)) {
				return;
			}

			var item=BSDatabase.instance.lobbyItemDatabase.createLobbyItem<LobbyItem> (lobbyItemID);
			UserJson.instance.addItem (item, UserJson.instance.inventoryName);
			lastBuyItem = item;
			BaseEventListener.onPublish ("ItemBuy");
		}
		public void buyItems(List<string> lobbyItemIDs) {
			if (!buyValidate(lobbyItemIDs.Count)) {
				return;
			}
			lastBuyItems.Clear ();
			for (int i = 0; i < lobbyItemIDs.Count; i++) {
				var item = BSDatabase.instance.lobbyItemDatabase.createLobbyItem<LobbyItem> (lobbyItemIDs[i]);
				UserJson.instance.addItem (item, UserJson.instance.inventoryName);
				lastBuyItems.Add (item);
			}
			BaseEventListener.onPublish ("ItemBuy");
		}

		protected bool buyValidate(int _needSpace) {
			if (UserJson.instance.getEmptySpace (UserJson.instance.inventoryName) <_needSpace) {
				BaseEventListener.onPublish ("NoSpace");
				return false;
			}
			if (type == CurrencyType.Money) {
				if (!UserJson.instance.useMoney (needValue)) {
					BaseEventListener.onPublish ("NoMoney");
					return false;
				}
			} else {
				if (!UserJson.instance.useGem (needValue)) {
					BaseEventListener.onPublish ("NoGem");
					return false;
				}
			}
			return true;
		}
	}
}

