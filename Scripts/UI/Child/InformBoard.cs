using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class InformBoard : Board
	{
		public override void Show() {
			base.Show ();

			sendBoolToReceiver ("Title", false);
			sendBoolToReceiver ("Text", false);
			sendBoolToReceiver ("Money", false);
			sendBoolToReceiver ("Food", false);
		}
		public void Show(string _title,string _text) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendBoolToReceiver ("Money", false);
			sendBoolToReceiver ("Food", false);
			sendToReceiver ("Text", _text);
		}
		public void Show(string _title,string _text,int _money,int _food) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendToReceiver ("Money", _money.ToString());
			sendToReceiver ("Food", _food.ToString());
			sendToReceiver ("Text", _text);
		}
	}
}

