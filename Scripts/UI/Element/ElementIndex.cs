using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BSS.UI {
	public class ElementIndex : ElementBase
	{
		public List<string> textList;
		public List<Sprite> spriteList;

		private void receiveIndexMessageEvent(ArgWithRecevier _messageArgs){
			if (_messageArgs.receiverName != elementName) {return;}
			int _index = (int)_messageArgs.parameter;
			updateIndexVariable (_index);
		}

		public void updateIndexVariable(int _index) {
			if (contentType == ContentType.Text) {
				if (textList.Count - 1 < _index) {
					return;
				}
				elementEnabled (true);
				updateVariable (textList [_index]);
			}
			if (contentType == ContentType.Image) {
				if (spriteList.Count - 1 < _index) {
					return;
				}
				elementEnabled (true);
				updateVariable (spriteList [_index]);
			}
		}
	}
}
