using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class ActItem : Activable
	{
		public string ID;
		public override Sprite icon {
			get {
				return item.icon;
			}
		}
		public override string titleContent {
			get {
				return item.itemName;
			}
		}
		public override string textContent {
			get {
				return item.itemDescription;
			}
		}

		private Item item;

		public override void initialize ()
		{
			item=BSDatabase.instance.items.database [ID];
		}
		public override void activate() {
			showInformDynamic ();
		}

	}
}

