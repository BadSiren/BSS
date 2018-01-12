using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS {
	public class PickUpItem : Photon.MonoBehaviour
	{
		public string ID;
		private Item item;

		void Start() {
			item=BSDatabase.instance.items.database [ID];
		}

		public void pickUp() {
			var unit=BaseSelect.instance.selectableList [0].owner;
			UnitUtils.AddItem (unit, ID);

			photonView.RPC ("recvDestroy", PhotonTargets.All);
		}
		[PunRPC]
		private void recvDestroy(PhotonMessageInfo mi) {
			Destroy (gameObject);
		}
	}
}

