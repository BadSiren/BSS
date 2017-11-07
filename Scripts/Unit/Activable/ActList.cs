using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	public class ActList : Activable
	{
		public List<Activable> activableList;
		public ActUndo actUndo;

		private ActiveBoard board;

		public override void onShow ()
		{
			base.onShow();
			if (board == null) {
				board=Board.boardList.Find (x => x.boardName == "ActiveBoard") as ActiveBoard;
				return;
			}
		}

		public override void activate(BaseUnit selectUnit) {
			base.activate (selectUnit);
			setActivable ();
		}

		private void setActivable() {
			if (activableList != null) {
				var temp = new List<Activable> ();
				temp.Add (actUndo);
				board.setActivableList (temp);
			}
		}
	}
}

