using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class InformBoard : Board
	{
        private GameObject sender;
		private System.Action buttonAct=null;
		private GameObject actObject;

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
        public void setSender(GameObject obj) {
            sender = obj;
        }

		public void setAction(GameObject target,string actName,System.Action action) {
			actObject = target;
			sendToReceiver ("Button", actName);
			sendBoolToReceiver ("Button", true);
			buttonAct = action;
		}
		public void activate() {
			if (actObject != null && buttonAct!=null) {
				buttonAct.Invoke ();
			}
			Close ();
		}


		public void Clear(){
			sendBoolToReceiver ("Title", false);
			sendBoolToReceiver ("Content", false);
			sendBoolToReceiver ("Icon", false);
			sendBoolToReceiver ("IconFrame", false);
			sendBoolToReceiver ("Button", false);
			actObject = null;
			buttonAct = null;
		}
	}
}

