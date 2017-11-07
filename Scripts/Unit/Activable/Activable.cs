using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	[System.Serializable]
	public class Activable : MonoBehaviour
	{
		public string actIndex;
		public Sprite buttonImage;
		[TextArea()]
		public string titleContent;
		[TextArea()]
		public string textContent;
		public bool costable;


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
			} else if (!costable) {
				UIController.instance.showInform (titleContent,textContent);
			} else {
				UIController.instance.showInform (titleContent,textContent,getMoney(),getFood());
			}
		}

		protected virtual int getMoney() {
			return 0;
		}
		protected virtual int getFood() {
			return 0;
		}

	}
}

