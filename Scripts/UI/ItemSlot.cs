using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class ItemSlot : Board
	{
		public int page;
		public string container;
		public int num;

		public System.Action act;


		public override void Show() {
			base.Show ();
			infoUpdate ();
		}
		public void infoUpdate() {
			if (!UserJson.instance.existsSlot (num, container)) {
				sendBoolToReceiver ("Icon" ,false);
				sendBoolToReceiver ("Text" ,false);
				return;
			}
			var item = UserJson.instance.getLobbyItem (num, container);
			sendToReceiver ("Icon" , Color.white);
			sendToReceiver("Icon",item.icon);
			sendToReceiver("Text",item.itemTitle);
			Color col=BSDatabase.instance.lobbyItemDatabase.rairityInfos [item.rairity].col;
			sendToReceiver("Text",col);
		}

		public void Execute() {
			if (!UserJson.instance.existsSlot (num, container)) {
				return;
			}
			if (act != null) {
				act ();
			}
		}
			
	}
}

