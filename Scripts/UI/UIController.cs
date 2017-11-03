using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {

	public class UIController : MonoBehaviour
	{
		public static UIController instance;
		public InformBoard informBoard;

		void Awake()
		{
			instance = this;
			if (informBoard == null) {
				Debug.Log ("InformBoard is null");
			}
			//informboard = Board.find ("InformBoard") as InformBoard;
		}
			
		public void clearInform() {
			if (informBoard == null) {
				Debug.Log ("InformBoard is null");
				return;
			}
			informBoard.Show ();
		}
		public void showInform(string _title,string _text) {
			if (informBoard == null) {
				Debug.Log ("InformBoard is null");
				return;
			}
			informBoard.Show (_title,_text);
		}
		public void showInform(string _title,string _text,int _money,int _food) {
			if (informBoard == null) {
				Debug.Log ("InformBoard is null");
				return;
			}
			informBoard.Show (_title,_text,_money,_food);
		}
	}
}

