using UnityEngine;
using System.Collections;
using BSS.LobbyItemSystem;

namespace BSS.UI {
	public class ItemInfoBoard : Board
	{
		private string containerName;
		private int num;
		public void Show(int _num,string _contain) {
			LobbyItem lobbyItem =UserJson.instance.getLobbyItem (_num, _contain);
			if (lobbyItem == null) {
				return;
			}

			base.Show ();
			containerName = _contain;
			num = _num;
			sendToReceiver ("Icon", lobbyItem.icon);
			sendToReceiver ("Title", lobbyItem.itemTitle);
			var rairInfo=BSDatabase.instance.lobbyItemDatabase.rairityInfos [lobbyItem.rairity];
			sendToReceiver ("Rairity", rairInfo.title);
			sendToReceiver ("Rairity", rairInfo.col);
			sendToReceiver ("Text", lobbyItem.itemDescription);
			if (lobbyItem is LobbyEquipItem) {
				sendToReceiver ("Type", "장착아이템");
				sendToReceiver ("Select", "장착하기");
			} else {
				sendToReceiver ("Type", "소모아이템");
				sendToReceiver ("Select", "사용하기");
			}
		}

		public void useItem() {
			LobbyItem lobbyItem =UserJson.instance.getLobbyItem (num, containerName);
			if (lobbyItem == null) {
				return;
			}
			foreach (var it in lobbyItem.lobbyActs) {
				it.activate (num, containerName);
			}
			if (lobbyItem is LobbyConsumeItem) {
				UserJson.instance.removeItem (num, containerName);
			}
		}
	}
}

