using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[System.Serializable]
	public abstract class Activable : SerializedMonoBehaviour
	{	
		[FoldoutGroup("ActBase(Mandatory)")]
		public bool isPrivate;
		[SerializeField]
		[FoldoutGroup("ActBase(Mandatory)")]
		public Sprite icon;

		[FoldoutGroup("ActBase(Mandatory)")]
		[SerializeField]
		[TextArea()]
		public string titleContent;
		[FoldoutGroup("ActBase(Mandatory)")]
		[SerializeField]
		[TextArea()]
		public string textContent;
		public string category {
			get {
				return activables.getCategory (this);
			}
		}
		public int index {
			get {
				return activables.getIndex (this);
			}
		}
		public Activables activables;
			
		void Start() {
			initialize ();
		}
		public abstract void initialize ();

		public abstract void activate ();

		public virtual void activateLongPress() {
			activate ();
		}	

		public virtual Sprite getIcon() {
			return icon;
		}
		public virtual string getTitle() {
			return titleContent;
		}
		public virtual string getText() {
			return textContent;
		}



	}
}

