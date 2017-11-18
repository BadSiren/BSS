using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	[System.Serializable]
	public class Activable : ScriptableObject
	{
		public string ID;
		public Sprite buttonImage;
		[SerializeField]
		[TextArea()]
		private string _titleContent;
		public virtual string titleContent {
			get {
				return _titleContent;
			}
		}
		[SerializeField]
		[TextArea()]
		private string _textContent;
		public virtual string textContent {
			get {
				return _titleContent;
			}
		}


		public virtual void onShow() {
		}
		public virtual void activate(BaseUnit selectUnit) {
			showInformDynamic ();
		}

		public virtual void activateLongPress(BaseUnit selectUnit) {
			showInformDynamic ();
		}	


		protected virtual void showInformDynamic() {
			if (string.IsNullOrEmpty (titleContent)) {
				UIController.instance.clearInform ();
			}  else {
				UIController.instance.showInform (titleContent,textContent);
			}
		}


	}
}

