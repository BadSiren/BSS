using UnityEngine;
using System.Collections;
using BSS.Unit;
using EventsPlus;

namespace BSS.UI {
	public class UnitBoard : Board
	{
		public BaseUnit selectUnit;

		protected override void initailze() {
			base.initailze ();
		}
		protected override void deInitailze() {
			base.deInitailze ();
		}

		public void Show(GameObject obj) {
			base.Show ();
			if (obj.GetComponent<BaseUnit> () != null) {
				setSelectUnit (obj.GetComponent<BaseUnit>());
			}
		}

		public void Show(BaseUnit unit) {
			base.Show ();
			setSelectUnit (unit);
		}
		public void CloseCheck(GameObject obj) {
			if (selectUnit != null && selectUnit.gameObject.GetInstanceID () == obj.GetInstanceID ()) {
				Close ();
			}
		}

		public virtual void setSelectUnit(BaseUnit unit) {
			selectUnit = unit;
		}
		public virtual void clearSelectUnit() {
			selectUnit = null;
		}
	}
}

