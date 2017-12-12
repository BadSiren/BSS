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
			sendBoolToReceiver ("Upgrade0", false);
			sendBoolToReceiver ("Upgrade1", false);
		}
		public void Show(string _title,string _text) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendToReceiver ("Text", _text);
			sendBoolToReceiver ("Money", false);
			sendBoolToReceiver ("Food", false);
			sendBoolToReceiver ("Upgrade0", false);
			sendBoolToReceiver ("Upgrade1", false);
		}
		public void Show(string _title,string _text,int _money,int _food) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendToReceiver ("Text", _text);
			sendToReceiver ("Money", _money.ToString());
			sendToReceiver ("Food", _food.ToString());
			sendBoolToReceiver ("Upgrade0", false);
			sendBoolToReceiver ("Upgrade1", false);
		}
		public void Show(string _title,string _text,int _money,int _food,Sprite _icon,int _need) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendToReceiver ("Text", _text);
			sendToReceiver ("Money", _money.ToString());
			sendToReceiver ("Food", _food.ToString());
			sendToReceiver ("Upgrade0", _icon);
			sendToReceiver ("Upgrade0", _need.ToString());
			sendBoolToReceiver ("Upgrade1", false);
		}
		public void Show(string _title,string _text,int _money,int _food,Sprite _icon0,int _need0,Sprite _icon1,int _need1) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendToReceiver ("Text", _text);
			sendToReceiver ("Money", _money.ToString());
			sendToReceiver ("Food", _food.ToString());
			sendToReceiver ("Upgrade0", _icon0);
			sendToReceiver ("Upgrade0", _need0.ToString());
			sendToReceiver ("Upgrade1", _icon1);
			sendToReceiver ("Upgrade1", _need1.ToString());
		}
	}
}

