using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class RandRange {
		public string containerName;
		public int rairity;//(value<0) is Random
		public List<float> rairtyProbs;
	}
	public class BuyRandEquipLobbyItem : MonoBehaviour
	{
		public enum CurrencyType
		{
			Money,Gem
		}
		//Rand Range
		public RandRange randRange;

		public CurrencyType type;
		public int needValue;

		public static LobbyEquipItem lastBuyItem;
		public static List<LobbyEquipItem> lastBuyItems=new List<LobbyEquipItem>();


		public void buyItem() {
			LobbyEquipItem _item = getItem (randRange);
			if (_item == null) {
				return;
			}
			if (UserJson.instance.getEmptySpace (UserJson.instance.inventoryName) <= 0) {
				BaseEventListener.onPublish ("NoSpace");
				return;
			}

			if (type == CurrencyType.Money) {
				if (UserJson.instance.useMoney (needValue)) {
					UserJson.instance.addItem (new UserJson.UserItem (_item.ID), UserJson.instance.inventoryName);
					lastBuyItem = _item;
					BaseEventListener.onPublish ("ItemBuy");
				} else {
					BaseEventListener.onPublish ("NoMoney");
					return;
				}
			} else if (type == CurrencyType.Gem) {
				if (UserJson.instance.useGem (needValue)) {
					UserJson.instance.addItem (new UserJson.UserItem (_item.ID), UserJson.instance.inventoryName);
					lastBuyItem = _item;
					BaseEventListener.onPublish ("ItemBuy");
				} else {
					BaseEventListener.onPublish ("NoGem");
					return;
				}
			}
		}
		public void buyItems(int _num) {
			List<LobbyEquipItem> _items = new List<LobbyEquipItem> ();
			while(true) {
				LobbyEquipItem _item = getItem (randRange);
				if (_item != null) {
					_items.Add (_item);
				} else {
					Debug.LogError ("Item Nothing");
					return;
				}
				if (_items.Count == _num) {
					break;
				}
			}
			if (UserJson.instance.getEmptySpace (UserJson.instance.inventoryName) <= _num - 1) {
				BaseEventListener.onPublish ("NoSpace");
				return;
			}

			if (type == CurrencyType.Money) {
				if (UserJson.instance.useMoney (needValue)) {
					lastBuyItems.Clear ();
					for (int i = 0; i < _num; i++) {
						UserJson.instance.addItem (new UserJson.UserItem (_items[i].ID), UserJson.instance.inventoryName);
						lastBuyItems.Add (_items [i]);
					}
					BaseEventListener.onPublish ("ItemBuy");
					return;
				} else {
					BaseEventListener.onPublish ("NoMoney");
					return;
				}
			} else if (type == CurrencyType.Gem) {
				if (UserJson.instance.useGem (needValue)) {
					lastBuyItems.Clear ();
					for (int i = 0; i < _num; i++) {
						UserJson.instance.addItem (new UserJson.UserItem (_items[i].ID), UserJson.instance.inventoryName);
						lastBuyItems.Add (_items [i]);
					}
					BaseEventListener.onPublish ("ItemBuy");
					return;
				} else {
					BaseEventListener.onPublish ("NoGem");
					return;
				}
			}
		}


		private LobbyEquipItem getItem(RandRange _randRange) {
			int _rairty = _randRange.rairity;
			if (_rairty < 0) {
				_rairty = chooseProb (_randRange.rairtyProbs);
			}
			List<LobbyEquipItem> items;
			if (string.IsNullOrEmpty (_randRange.containerName)) {
				items = BSDatabase.instance.lobbyItemDatabase.getLobbyEquipItems ().FindAll (x => x.rairity == _rairty);
			} else {
				items = BSDatabase.instance.lobbyItemDatabase.getLobbyEquipItems ().FindAll (x => x.containerName == _randRange.containerName && x.rairity == _rairty);
			}
			if (items==null || items.Count == 0) {
				Debug.Log ("No Item");
				return null;
			}
			return items[Random.Range (0, items.Count)];
		}

		private int chooseProb (List<float> probs) {
			if (probs == null || probs.Count==0) {
				return 0;
			}
			float total = 0;
			foreach (var it in probs) {
				total += it;
			}
			float randomPoint = Random.value * total;

			for (int i= 0; i < probs.Count; i++) {
				if (randomPoint < probs[i]) {
					return i;
				}
				else {
					randomPoint -= probs[i];
				}
			}
			return probs.Count - 1;
		}
	}
}

