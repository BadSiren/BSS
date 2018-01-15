using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;
using BSS.UI;

namespace BSS {
	public class PickUpItem : Photon.MonoBehaviour
	{
		public string ID;

		private Item item;

		void Start() {
			item=BSDatabase.instance.items.database [ID];
		}

		public static PickUpItem Create(string _ID,Vector2 pos) {
			var obj=UnitUtils.CreatePunObject ("PickUpItem", pos);
			var pickUpItem=obj.GetComponent<PickUpItem> ();
			pickUpItem.ID = _ID;
			return pickUpItem;
		}

		public void showInform() {
			var informBoard = Board.boardList.Find (x => x is InformBoard) as InformBoard;
			informBoard.Show (item.itemName, item.itemDescription, item.icon);
			informBoard.setAction (gameObject,"줍기",pickUp);
		}
		public void pickUp() {
			if (BaseSelect.instance.eSelectState != ESelectState.Mine) {
				return;
			}
			var unit = BaseSelect.instance.selectableList [0].owner;
			if (unit.GetComponent<Itemable> () == null) { 
				return;
			}
			unit.photonView.RPC ("recvAddItem", PhotonTargets.All, ID);
			photonView.RPC ("recvDestroy", PhotonTargets.All);
		}

		[PunRPC]
		private void recvDestroy(PhotonMessageInfo mi) {
			Destroy (gameObject);
		}
	}
}

