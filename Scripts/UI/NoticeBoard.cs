using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class NoticeBoard : Board
	{
		private System.Action action;

        public static void Notice(string _text) {
            var notice = boardList.Find(x => x is NoticeBoard) as NoticeBoard;
            notice.Show(_text);
        }
        public static void Notice(string _text, System.Action act) {
            var notice = boardList.Find(x => x is NoticeBoard) as NoticeBoard;
            notice.Show(_text,act);
        }
		public void Show(string _text) {
			base.Show ();
			sendToReceiver ("Text", _text);
		}
		public void Show(string _text,System.Action act) {
			action = act;
			Show (_text);
		}
		public override void Close() {
			base.Close ();
			action = null;
		}
		public void activate() {
			if (action != null) {
				action ();
			}
		}
		public void actAndClose() {
			if (action != null) {
				action ();
			}
			Close ();
		}
	}
}
