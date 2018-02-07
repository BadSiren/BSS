using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
    public class NotifyBoard : TimerBoard
	{
        public Dictionary<string, string> notifyDics = new Dictionary<string, string>();
        private string lastedText = "";

        public static void Notify(string notifyID) {
            var notifyBoard = boardList.Find(x => x is NotifyBoard) as NotifyBoard;
            if (!notifyBoard.notifyDics.ContainsKey(notifyID)) {
                return;
            }
            notifyBoard.Show(notifyBoard.notifyDics[notifyID]);
        }
		public void Show(string _text) {
			base.Show ();
            lastedText = lastedText + _text+"\n";
            sendToReceiver ("Text", lastedText);
		}

        public override void Close() {
            base.Close();
            lastedText = "";
        }


	}
}

