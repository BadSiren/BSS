using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
	public class TextBoard : Board
	{	
		public void Show(string _text) {
			base.Show ();
			sendToReceiver ("Text", _text);
		}



	}
}

