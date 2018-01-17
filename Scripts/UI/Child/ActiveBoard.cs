using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.UI {
	public class ActiveBoard : UnitBoard
	{
		public const int MAX_COUNT = 9;
		public string selectCategory="Base";


		public override void changeSelectUnit (BaseUnit unit)
		{
			selectUnit = unit;
			changeCategory ("Base");
		}
		public override void clearSelectUnit ()
		{
			sendBoolToReceiver ("All", false);
		}
		public void changeCategory(string category) {
			if (selectUnit == null) {
				return;
			}
			selectCategory = category;

			for (int i=0;i<MAX_COUNT;i++) {
				clearActButtonImage (i);
				var act=selectUnit.activables.getActivableOrNull (category, i);

				if (act != null && act.getIcon()!=null) {
					setActButtonImage (i, act.getIcon(), act.getTitle());
				}
			}
		}
		public void updateActivable() {
			changeCategory (selectCategory);
		}

		public void activeButton(int num) {
			if (selectUnit == null || selectUnit.activables.getActivableOrNull (selectCategory, num) == null) {
				return;
			}
			if (selectUnit.activables.getActivableOrNull (selectCategory, num).isPrivate && !selectUnit.isMine ) {
				return;
			}
			selectUnit.activables.getActivableOrNull (selectCategory, num).activate ();
		}

		public void activeButtonLongPress(int num) {
			if (selectUnit == null || selectUnit.activables.getActivableOrNull (selectCategory, num) == null) {
				return;
			}
			if (selectUnit.activables.getActivableOrNull (selectCategory, num).isPrivate && !selectUnit.isMine) {
				return;
			}
			selectUnit.activables.getActivableOrNull (selectCategory, num).activateLongPress ();
		}

		private void setActButtonImage(int index,Sprite icon,string title) {
			sendToReceiver ("ButtonTitle" + index.ToString (), title);
			sendToReceiver ("ButtonIcon" + index.ToString (), icon);
		}
		private void clearActButtonImage(int index) {
			sendBoolToReceiver ("ButtonTitle" + index.ToString (), false);
			sendBoolToReceiver ("ButtonIcon" + index.ToString (), false);
		}
	}
}

