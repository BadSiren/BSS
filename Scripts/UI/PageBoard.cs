using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class PageBoard : Board
	{
		public int page;
		public int maxPage;

		public override void Show() {
			base.Show ();
			pageViewUpdate ();
		}

		public void pageUp() {
			if (maxPage <= page) {
				return;
			}
			page++;
			Show ();
		}
		public void pageDown() {
			if (0 >= page) {
				return;
			}
			page--;
			Show ();
		}
		public virtual void pageViewUpdate() {
			sendToReceiver ("Page", page.ToString ());
		}
	}
}

