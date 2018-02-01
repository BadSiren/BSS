using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.UI {
	public abstract class UnitBoard : Board
	{
		public static List<UnitBoard> unitBoardList = new List<UnitBoard>();
		public BaseUnit selectUnit;

		public void changeSelectUnit (GameObject obj) {
			var unit=obj.GetComponent<BaseUnit> ();
			changeSelectUnit (unit);
		}
		public abstract void changeSelectUnit (BaseUnit unit);
		public abstract void clearSelectUnit ();

		protected override void initialize() {
			base.initialize ();
			unitBoardList.Add (this);
		}
		protected override void deInitialize() {
            base.deInitialize ();
			unitBoardList.Remove (this);
		}
	}
}

