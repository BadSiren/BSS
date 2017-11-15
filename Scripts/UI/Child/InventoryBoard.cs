using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class InventoryBoard : Board
	{
		public int page;

		public override void Show() {
			base.Show ();

			sendBoolToReceiver ("Title", false);
			sendBoolToReceiver ("Text", false);
			sendBoolToReceiver ("Money", false);
			sendBoolToReceiver ("Food", false);
		}
	}
}
