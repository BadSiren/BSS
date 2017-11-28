using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[System.Serializable]
	public class Activable : SerializedScriptableObject
	{
		public string ID;
		public Sprite buttonImage;
		[SerializeField]
		[TextArea()]
		public string titleContent;
		[SerializeField]
		[TextArea()]
		public string textContent;


		public virtual void initialize(Dictionary<string,string> args) {
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

