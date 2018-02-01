using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class PageBoard : Board
	{
		public int page;
		public int maxPage;

		public override void Show() {
			base.Show ();
			pageUpdate ();
		}

		public void pageUp() {
			if (maxPage <= page) {
				return;
			}
			page++;
			pageUpdate ();
		}
		public void pageDown() {
			if (0 >= page) {
				return;
			}
			page--;
			pageUpdate ();
		}
		public virtual void pageUpdate() {
            sendToReceiver("Page", page.ToString());
		}
	}
}

