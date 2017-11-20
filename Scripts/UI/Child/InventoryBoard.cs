using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;
using UnityEngine.UI;

namespace BSS.UI {
	public class InventoryBoard : ContainerBoard
	{
		public enum InvenState {
			Info,Change,Remove
		}
		public InvenState state;

		private int preSlot = -1;


		public override void Show() {
			base.Show ();
			for (int i = 0; i < viewSlot; i++) {
				if (page * viewSlot + i == preSlot) {
					sendToReceiver("Icon"+i.ToString(),Color.red);
				}
			}
		}

		public override void activeButton(int _num) {
			int pageNum = page * viewSlot + _num;
			if (getCount() - 1 < pageNum) {
				return;
			}
			if (state == InvenState.Info) {
				ItemInfoBoard infoBoard=Board.boardList.Find (x => x is ItemInfoBoard) as ItemInfoBoard;
				infoBoard.Show (pageNum, containerName);
			}
			if (state == InvenState.Change) {
				if (preSlot == -1) {
					preSlot = pageNum;
				} else {
					UserJson.instance.changeItem (preSlot,pageNum,containerName);
					preSlot = -1;
				}
			}
			if (state == InvenState.Remove) {
				UserJson.instance.removeItem (pageNum,containerName);
			}
			Show ();
		}

		public override void activeButtonLongPress(int _num) {
			if (getCount() - 1 < _num) {
				return;
			}
			Show ();
		}
		public void setState(string _state) {
			preSlot = -1;
			state = (InvenState)System.Enum.Parse (typeof(InvenState), _state);
			Show ();
		}
	}
}
