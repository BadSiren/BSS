using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
	public class SelectBoard : Board
	{
		public static bool isSelecting=false;
		private System.Action selectAction;


		public void Show (string title,string text,Sprite icon,System.Action _action)
		{
			base.Show ();
			sendToReceiver ("Icon", icon);
			sendToReceiver ("Title", title);
			sendToReceiver ("Text", text);
			selectAction = _action;
			isSelecting = true;
		}

		public void actSelect() {
			if (selectAction != null) {
				selectAction ();
			}
			isSelecting = false;
			Close ();
		}

	}
}

