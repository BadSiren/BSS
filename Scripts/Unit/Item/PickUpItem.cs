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
			if (BaseSelect.instance.eSelectState != ESelectState.Mine) {
				return;
			}
			var itemable = BaseSelect.instance.mainSelectable.owner.GetComponent<Itemable> ();
			if (itemable == null) {
				return;
			}
			var informBoard = Board.boardList.Find (x => x is InformBoard) as InformBoard;
			informBoard.Show (item.itemName, item.itemDescription, item.icon);
		
			informBoard.setAction (gameObject, "줍기", () => {
				itemable.addItem(ID);
				photonView.RPC("recvDestroy",PhotonTargets.All);
			});

		}
		[PunRPC]
		private void recvDestroy(PhotonMessageInfo mi) {
			Destroy (gameObject);
		}
	}
}

