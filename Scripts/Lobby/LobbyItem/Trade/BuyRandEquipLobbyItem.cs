using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.LobbyItemSystem {
	public class BuyRandEquipLobbyItem : MonoBehaviour
	{
		public enum CurrencyType
		{
			Money,Gem
		}
		//Rand Range
		public string containerName;
		public int rairity;//(value<0) is Random
		public List<float> rairtyProbs;

		public CurrencyType type;
		public int needValue;

		public void buyItem() {
			int _rairty = rairity;
			if (_rairty < 0) {
				_rairty = chooseProb (rairtyProbs);
			}

			List<LobbyEquipItem> items;
			if (string.IsNullOrEmpty (containerName)) {
				items = BSDatabase.instance.lobbyItemDatabase.getLobbyEquipItem ().FindAll (x => x.rairity == _rairty);
			} else {
				items = BSDatabase.instance.lobbyItemDatabase.getLobbyEquipItem ().FindAll (x => x.containerName == containerName && x.rairity == _rairty);
			}
			Debug.Log (items.Count);

			if (items==null || items.Count == 0) {
				Debug.Log ("No Item");
				return;
			}

			if (type == CurrencyType.Money) {
				if (UserJson.instance.getEmptySpace (UserJson.instance.inventoryName) > 0 && UserJson.instance.useMoney (needValue)) {
					int _index=Random.Range (0, items.Count);
					UserJson.instance.addItem (new UserJson.UserItem (items[_index].ID), UserJson.instance.inventoryName);
				} else {
					Debug.Log (11);
				}

			} else if (type == CurrencyType.Gem) {
				if (UserJson.instance.getEmptySpace (UserJson.instance.inventoryName)>0 && UserJson.instance.useGem (needValue)) {
					int _index=Random.Range (0, items.Count);
					UserJson.instance.addItem (new UserJson.UserItem (items[_index].ID), UserJson.instance.inventoryName);
				} else {
					Debug.Log (11);
				}
			}
		}

		public int chooseProb (List<float> probs) {
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

