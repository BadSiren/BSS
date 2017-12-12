using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class EquipmentSlotContainer : ItemSlotContainer
	{
		protected override void initialize() {
			base.initialize ();
			foreach (var it in slots) {
				it.act = () => {
					UserJson.instance.changeContainer (it.num, it.container, UserJson.instance.inventoryName);
				};
			}
		}
	}
}

