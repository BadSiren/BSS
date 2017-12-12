using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
	public class ItemSlotContainer : PageBoard
	{
		public List<ItemSlot> slots;

		public override void Show() {
			base.Show ();
			ShowSlot (page);
		}

		public override void pageViewUpdate() {
			base.pageViewUpdate ();
			ShowSlot (page);
		}
		public virtual void infoUpdate() {
			foreach (var it in slots) {
				it.infoUpdate ();
			}
		}

		private void ShowSlot(int _page) {
			foreach (var it in slots.FindAll(x=>x.page==_page)) {
				it.Show ();
			}
			foreach (var it in slots.FindAll(x=>x.page!=_page)) {
				it.Close ();
			}
		}
	}
}

