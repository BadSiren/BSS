using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
	public class SelectBoard : Board
	{
		private System.Action selectAction;


		public void Show (string title,Sprite icon,System.Action _action)
		{
			base.Show ();
			sendToReceiver ("Icon", icon);
			sendToReceiver ("Text", title);
			selectAction = _action;
		}

		public void actSelect() {
			if (selectAction != null) {
				selectAction ();
			}
			Close ();
		}

	}
}

