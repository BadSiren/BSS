using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	public class BuyLobbyItemRand : BuyLobbyItem
	{
		public string containerName;
		public List<float> rairtyProbs;

		public void buyItemRand() {
			buyItem (getLobbyItemID ());
		}
		public void buyItemsRand(int num) {
			List<string> ids = new List<string> ();
			for (int i = 0; i < num; i++) {
				ids.Add (getLobbyItemID ());
			}
			buyItems (ids);
		}

		//Container , RandRairity
		private string getLobbyItemID() {
			var equips = BSDatabase.instance.lobbyItemDatabase.getLobbyItems<LobbyEquipItem> ();
			int _rairity = chooseNum (rairtyProbs);
			var rands=equips.FindAll (x => x.containerName == containerName && x.rairity == _rairity);
			if (rands.Count == 0) {
				Debug.LogError ("No Item");
			}
			return rands [Random.Range (0, rands.Count)].ID;
		}
			
		private int chooseNum (List<float> probs) {
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

