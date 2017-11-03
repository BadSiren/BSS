using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	public class ActList : Activable
	{
		public List<Activable> activableList;

		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			setActivable ();
		}

		private void setActivable() {
			if (activableList != null) {
				ActiveBoard board=Board.boardList.Find (x => x.boardName == "ActiveBoard") as ActiveBoard;
				if (board == null) {
					Debug.Log ("ActiveBoard not found");
					return;
				}
				board.setActivableList (activableList);
			}
		}
	}
}

