using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS.UI {
	public class EnterSelect : PageBoard {

		public int mode=0;

		public override void Show() {
			base.Show ();
			pageViewUpdate ();
		}

		public override void pageViewUpdate() {
			var levelInfo=BSDatabase.instance.levelDatabase.levelInfos [mode.ToString () + "/" + page.ToString ()];

			sendToReceiver ("Title", page.ToString()+"단계 ["+levelInfo.title+"]");
			sendToReceiver ("MaxLevel", levelInfo.maxLevel.ToString());
			sendToReceiver ("ClearMoney", levelInfo.clearMoney.ToString());

			sendToReceiver ("Page", page.ToString ());
		}
		public override void pageUpdate ()
		{
			base.pageUpdate ();
			PlayManager.difficulty = page;
		}

		private bool validate() {
			return true;
		}
	}
}