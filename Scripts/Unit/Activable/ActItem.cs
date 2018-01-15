using UnityEngine;
using System.Collections;
using BSS.UI;

namespace BSS.Unit {
	public class ActItem : Activable
	{
		public string ID;

		private Item item;
		private Itemable itemable;

		public override void initialize ()
		{
			itemable = owner.GetComponent<Itemable> ();
			item=BSDatabase.instance.items.database [ID];
			titleContent = item.itemName;
			textContent = item.itemDescription;
			icon = item.icon;
		}
		public override void activate() {
			var informBoard = Board.boardList.Find (x => x is InformBoard) as InformBoard;
			informBoard.Show (titleContent,textContent,icon);
			informBoard.setAction (gameObject,"버리기",() => {
				owner.photonView.RPC("recvThrowItem",PhotonTargets.All,index);
			});
		}

	}
}

