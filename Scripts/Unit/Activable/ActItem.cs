using UnityEngine;
using System.Collections;
using BSS.UI;

namespace BSS.Unit {
    public class ActItem : Activable {

        private static int dragedItem=-1;
		private Itemable itemable;

		public override void initialize ()
		{
			itemable = GetComponentInParent<Itemable> ();
		}
		public override void activate() {
			if (itemable.getItemOrNull (index) == null) {
				return;
			}
			var informBoard = Board.boardList.Find (x => x is InformBoard) as InformBoard;
			informBoard.Show (getTitle(),getText(),getIcon());
			informBoard.setAction (gameObject,"버리기",() => {
				itemable.throwItem(index);
			});
		}

        public override void activateLongPress() {
            if (itemable.getItemOrNull(index) == null) {
                return;
            }
            dragedItem = index;
        }

		public override Sprite getIcon ()
		{
			if (itemable.getItemOrNull (index) == null) {
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

