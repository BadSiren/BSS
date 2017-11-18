using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;
using UnityEngine.UI;

namespace BSS.UI {
	public class EquipmentBoard : ContainerBoard
	{
		public override void Show() {
			base.Show ();
		}

		public override void activeButton(int _num) {
			int pageNum = page * viewSlot + _num;
			if (getCount() - 1 < pageNum) {
				return;
			}
			UserJson.instance.transportItem (pageNum, containerName, UserJson.instance.inventoryName);
			InventoryBoard inven=Board.boardList.Find (x => x is InventoryBoard) as InventoryBoard;
			inven.Show ();
			Show ();
		}

		public override void activeButtonLongPress(int _num) {
			if (getCount() - 1 < _num) {
				return;
			}
			Show ();
		}
	}
}

