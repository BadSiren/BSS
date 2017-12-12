using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class InventorySlotContainer : ItemSlotContainer
	{
		public enum EInvenState {
			Info,Change,Remove
		}
		public EInvenState eInvenState;

		private int preSlot = -1;

		protected override void initialize() {
			base.initialize ();
			for (int i=0;i<slots.Count;i++) {
				slots[i].num = i;
			}
			foreach (var it in slots) {
				it.act = () => {
					ItemInfoBoard infoBoard=Board.boardList.Find (x => x is ItemInfoBoard) as ItemInfoBoard;
					infoBoard.Show (it.num, it.container);
				};
			}
		}
		public override void infoUpdate() {
			base.infoUpdate ();
			if (preSlot != -1) {
				slots.Find (x => x.num == preSlot).sendToReceiver ("Icon", Color.red);
			}
		}


		public void setState(string _state) {
			preSlot = -1;
			eInvenState = (EInvenState)System.Enum.Parse (typeof(EInvenState), _state);
			infoUpdate ();

			//State Act
			switch (eInvenState) {
			case EInvenState.Info:
				foreach (var it in slots) {
					it.act = () => {
						ItemInfoBoard infoBoard = Board.boardList.Find (x => x is ItemInfoBoard) as ItemInfoBoard;
						infoBoard.Show (it.num, it.container);
					};
				}
				break;
			case EInvenState.Change:
				foreach (var it in slots) {
					it.act = () => {
						if (preSlot == -1) {
							preSlot = it.num;
						} else {
							UserJson.instance.changeSlot (preSlot,it.num,it.container);
							preSlot = -1;
						}
						infoUpdate();
					};
				}
				break;
			case EInvenState.Remove:
				foreach (var it in slots) {
					it.act = () => {
						UserJson.instance.removeItem (it.num,it.container);
					};
				}
				break;
			}
		}
	}
}

