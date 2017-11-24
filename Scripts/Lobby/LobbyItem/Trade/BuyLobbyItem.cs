using UnityEngine;
using System.Collections;

namespace BSS.LobbyItemSystem {
	public class BuyLobbyItem : MonoBehaviour
	{
		public enum CurrencyType
		{
			Money,Gem
		}
		public string lobbyItemID;
		public CurrencyType type;
		public int needValue;

		public void buyItem() {
			var item=BSDatabase.instance.lobbyItemDatabase.lobbyItems.Find (x => x.ID == lobbyItemID);
			if (item == null) {
				Debug.Log ("No Item");
				return;
			}
			if (UserJson.instance.getEmptySpace (UserJson.instance.inventoryName) <=0) {
				BaseEventListener.onPublish ("NoSpace");
				return;
			}

			if (type == CurrencyType.Money) {
				if (UserJson.instance.useMoney (needValue)) {
					UserJson.instance.addItem (new UserJson.UserItem (lobbyItemID), UserJson.instance.inventoryName);
				} else {
					BaseEventListener.onPublish ("NoMoney");
				}

			} else if (type == CurrencyType.Gem) {
				if (UserJson.instance.useGem (needValue)) {
					UserJson.instance.addItem (new UserJson.UserItem (lobbyItemID), UserJson.instance.inventoryName);
				} else {
					BaseEventListener.onPublish ("NoGem");
				}
			}

		}
	}
}

