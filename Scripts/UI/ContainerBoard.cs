using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using BSS.LobbyItemSystem;

namespace BSS.UI {
	public abstract class ContainerBoard : PageBoard
	{
		public string containerName;
		public int viewSlot;

		public override void Show() {
			base.Show ();
			for (int i = 0; i < viewSlot; i++) {
				sendBoolToReceiver("Icon"+i.ToString(),false);
				sendBoolToReceiver("Text"+i.ToString(),false);
				sendBoolToReceiver("Grade"+i.ToString(),false);
			}
			for (int i = 0; i < Mathf.Min(getCountPage(),viewSlot); i++) {
				sendToReceiver ("Icon" + i.ToString (), Color.white);
				sendToReceiver("Icon"+i.ToString(),getIcon(i+page*viewSlot));
				sendToReceiver("Text"+i.ToString(),getTitle(i+page*viewSlot));
				int _rairity = getRairity(i + page * viewSlot);
				Color col=BSDatabase.instance.lobbyItemDatabase.rairityInfos [_rairity].col;
				sendToReceiver("Text"+i.ToString(),col);
			}
		}

		public abstract void activeButton (int _num);
		public abstract void activeButtonLongPress (int _num);
		//
		protected virtual string getTitle(int num) {
			var item = UserJson.instance.getLobbyItem (num, containerName);
			if (item == null) {
				return "";
			}
			return UserJson.instance.getLobbyItem (num, containerName).itemTitle;
		}
		protected virtual Sprite getIcon (int num) {
			var item = UserJson.instance.getLobbyItem (num, containerName);
			if (item == null) {
				return null;
			}
			return UserJson.instance.getLobbyItem (num, containerName).icon;
		}
		protected virtual int getRairity (int num) {
			var item = UserJson.instance.getLobbyItem (num, containerName);
			if (item == null) {
				return 0;
			}
			return UserJson.instance.getLobbyItem (num, containerName).rairity;
		}

		protected virtual int getCount () {
			return UserJson.instance.containers[containerName].Count;
		}
		protected virtual int getCountPage() {
			return getCount()-page*viewSlot;
		}
	}
}
