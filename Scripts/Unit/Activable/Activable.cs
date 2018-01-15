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
		[FoldoutGroup("ActBase(Mandatory)")]
		public string category="Base";
		[FoldoutGroup("ActBase(Mandatory)")]
		public int index;
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


		protected BaseUnit owner;

		void Awake() {
			owner = GetComponentInParent<BaseUnit> ();
		}
		void Start() {
			initialize ();
		}
		public abstract void initialize ();


		public virtual bool validate() {
			return true;
		}

		public abstract void activate ();

		public virtual void activateLongPress() {
			activate ();
		}	





	}
}

