using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class InformBoard : Board
	{
		public void Show(string title,string content) {
			base.Show ();

			Clear ();
			sendToReceiver ("Title", title);
			sendToReceiver ("Content", content);
		}
		public void Show(string title,string content,Sprite icon) {
			base.Show ();

			Clear ();
			sendToReceiver ("Title", title);
			sendToReceiver ("Content", content);
			sendToReceiver ("Icon", icon);
			sendBoolToReceiver ("IconFrame", true);
		}


		public void Clear(){
			sendBoolToReceiver ("Title", false);
			sendBoolToReceiver ("Content", false);
			sendBoolToReceiver ("Icon", false);
			sendBoolToReceiver ("IconFrame", false);
			sendBoolToReceiver ("Button", false);
		}
	}
}

