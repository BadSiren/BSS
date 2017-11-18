using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	public class ActListSkills : Activable
	{
		/*
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
			setActivable (selectUnit);
		}
		private void setActivable(BaseUnit selectUnit) {
			List<Activable> activableList = new List<Activable> ();
			foreach (var it in selectUnit.skillList) {
				Activable activable=new Activable ();
				activable.buttonImage = it.skillBase.skillImage;
				activable.titleContent = it.skillBase.skillName;
				activable.textContent = it.skillBase.skillText;
				activableList.Add (activable);
			}
			activableList.Add (actUndo);
			board.setActivableList (activableList);
		}
		*/
	}
}

