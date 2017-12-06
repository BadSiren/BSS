using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.LobbyItemSystem {
	public class LobbyActTransport : LobbyAct
	{
		public bool isContainerShow=true;

		public override void activate (int _num,string _containerName) {
			LobbyItem _item=UserJson.instance.getLobbyItem (_num, _containerName);
			LobbyEquipItem item = _item as LobbyEquipItem;
			if (item == null) {
				return;
			}

			if (!UserJson.instance.changeContainer (_num, _containerName, item.containerName)) {
				BaseEventListener.onPublish ("NoSpace");
			}
			if (isContainerShow) {
				containerShow (_containerName);
				containerShow (item.containerName);
			}

		}
		private void containerShow(string _containerName) {
			var boards = Board.boardList.FindAll (x => x is ContainerBoard);
			List<ContainerBoard> contains = new List<ContainerBoard> ();
			foreach (var it in boards) {
				contains.Add(it as ContainerBoard);
			}
			var contain =contains.Find (x => x.containerName == _containerName);
			if (contain == null) {
				return;
			}
			contain.Show ();
		}


	}
}

