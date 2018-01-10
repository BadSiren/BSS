using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS.UI {
	public class EnterSelect : PageBoard {

		public int mode=0;
		public DicSelector dicSelector;

		public override void Show() {
			base.Show ();
			pageViewUpdate ();
		}

		public override void pageViewUpdate() {
			var levelInfo = dicSelector.getDic (page);

			sendToReceiver ("Title", levelInfo ["Title"]);
			sendToReceiver ("BossName", levelInfo ["BossName"]);
			sendToReceiver ("Stage", levelInfo ["Stage"]);
			sendToReceiver ("BossSpr", dicSelector.convertSprite(levelInfo ["BossSpr"]));

			sendToReceiver ("Page", page.ToString ());
		}

		private bool validate() {
			return true;
		}
	}
}