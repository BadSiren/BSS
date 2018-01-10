using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	[System.Serializable]
	public class ActDescription : Activable
	{
		public override void initialize() {}

		public override void activate() {
			showInformDynamic ();
		}


		public override string infoContent {
			get {
				return titleContent;
			}
		}	
		public void setSprite(Sprite spr) {
			_icon = spr;
		}
		public void setTitle(string title) {
			_titleContent = title;
		}
		public void setText(string text) {
			_textContent = text;
		}
	}
}

