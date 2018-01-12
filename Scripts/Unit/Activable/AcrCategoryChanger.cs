using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	public class AcrCategoryChanger : Activable
	{
		public string changeCategory;

		public override void initialize ()
		{
			
		}
		public override void activate() {
			var activeBoard = Board.boardList.Find (x => x is ActiveBoard) as ActiveBoard;
			activeBoard.clearSelectUnit ();
			activeBoard.selectUnit = owner;
			activeBoard.changeCategory (changeCategory);
		}

	}
}


