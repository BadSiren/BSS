using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.UI {
	public abstract class UnitBoard : Board
	{
		public static List<UnitBoard> unitBoardList = new List<UnitBoard>();
		public BaseUnit selectUnit;

		protected override void initialize() {
			base.initialize ();
			unitBoardList.Add (this);
		}
		protected override void deInitailze() {
			base.deInitailze ();
			unitBoardList.Remove (this);
		}
			
		public abstract void changeSelectUnit (BaseUnit unit);
		public abstract void clearSelectUnit ();


		public void changeSelectUnit(GameObject obj) {
			var _unit=obj.GetComponent<BaseUnit> ();
			changeSelectUnit (_unit);
		}
	}
}

