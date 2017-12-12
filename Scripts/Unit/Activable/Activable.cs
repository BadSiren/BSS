using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public class ActivableInfo {
		public string ID;
		public Sprite icon;
		public string titleContent;
		[TextArea()]
		public string textContent;
	}
	[System.Serializable]
	public class Activable : SerializedScriptableObject
	{
		public string ID;
		public Sprite icon;
		[TextArea()]
		public string titleContent;
		[TextArea()]
		public string textContent;
		public virtual string infoContent {
			get {
				return "";
			}
		}


		public virtual void initialize(Dictionary<string,string> args) {
		}


		public virtual void activate(BaseUnit selectUnit) {
			showInformDynamic ();
		}

		public virtual void activateLongPress(BaseUnit selectUnit) {
			showInformDynamic ();
		}	

		public virtual bool validate() {
			return true;
		}

		protected virtual void showInformDynamic() {
			if (string.IsNullOrEmpty (titleContent)) {
				UIController.instance.informBoard.Close ();
			}  else {
				UIController.instance.informBoard.Show (titleContent,textContent);
			}
		}


	}
}

