using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.UI {

	public class UIController : MonoBehaviour
	{
		public static UIController instance;
		public InformBoard informBoard;
		public UnitInfo unitInfo;
		public ActiveBoard activeBoard;
		public NotifyBoard notifyBoard;

		void Awake()
		{
			instance = this;
			if (informBoard == null || unitInfo==null || activeBoard==null) {
				Debug.LogError ("Board is null");
			}
		}
			
		public void clearInform() {
			informBoard.Close ();
		}
		public void showInform(string _title,string _text) {
			informBoard.Show (_title,_text);
		}
		public void showInform(string _title,string _text,int _money,int _food) {
			informBoard.Show (_title,_text,_money,_food);
		}

	}
}

