using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class UIControl : MonoBehaviour
	{
		public void unitInfoShow() {
			var unitInfo=Board.boardList.Find (x => x is UnitInfo) as UnitInfo;
			unitInfo.Show (gameObject);
		}
		public void unitInfoClear() {
			var unitInfo=Board.boardList.Find (x => x is UnitInfo) as UnitInfo;
			unitInfo.clearSelectUnit ();
		}
	}
}

