using UnityEngine;
using System.Collections;
using BSS.UI;
using UnityEngine.EventSystems;

namespace BSS.Unit {
    public class ActItem : Activable {
		private Itemable itemable;
        private Vector2 prePos;

		public override void initialize ()
		{
			itemable = GetComponentInParent<Itemable> ();
		}
		public override void activate() {
            if (itemable.getItemOrNull (index) == null || !checkInteractable()) {
				return;
			}
            var pickUpItemBoard = Board.boardList.Find(x => x is PickUpItemBoard) as PickUpItemBoard;

            if (itemable.selectedItem != -1) {
                if (itemable.selectedItem != index) {
                    itemable.swapItem(itemable.selectedItem, index);
                }
                itemable.selectedItem = -1;
                return;
            }
            itemable.selectedItem = -1;
		}

        public override void activateLongPress() {
            if (itemable.getItemOrNull(index) == null || !checkInteractable()) {
                return;
            }
            itemable.selectedItem = index;
        }

		public override Sprite getIcon ()
		{
            if (itemable.getItemOrNull (index) == null ) {
				return null;
			}
			return itemable.getItemOrNull (index).icon;
		}
		public override string getTitle ()
		{
			if (itemable.getItemOrNull (index) == null) {
				return "";
			}
			return itemable.getItemOrNull (index).itemName;
		}
		public override string getText ()
		{
			if (itemable.getItemOrNull (index) == null) {
				return "";
			}
			return itemable.getItemOrNull (index).itemDescription;
		}

       

	}
}

