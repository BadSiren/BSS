using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[System.Serializable]
	public abstract class Activable : SerializedMonoBehaviour
	{	
		[FoldoutGroup("ActBase(Mandatory)")]
		public string category="Base";
		[SerializeField]
		[FoldoutGroup("ActBase(Mandatory)")]
		public Sprite _icon;
		public virtual Sprite icon {
			get {
				return _icon;
			}
		}
		[FoldoutGroup("ActBase(Mandatory)")]
		[SerializeField]
		[TextArea()]
		public string _titleContent;
		public virtual string titleContent {
			get {
				return _titleContent;
			}
		}
		[FoldoutGroup("ActBase(Mandatory)")]
		[SerializeField]
		[TextArea()]
		public string _textContent;
		public virtual string textContent {
			get {
				return _textContent;
			}
		}
		[FoldoutGroup("ActBase(Mandatory)")]
		public bool isPrivate;

		public virtual string infoContent {
			get {
				return titleContent;
			}
		}
		protected BaseUnit owner;

		void Awake() {
			owner = GetComponentInParent<BaseUnit> ();
		}
		void Start() {
			initialize ();
		}

		public static T addComponent<T>(BaseUnit target) where T : Activable  {
			return target.gameObject.transform.Find ("Activable").gameObject.AddComponent <T>() ;
		}
		public abstract void initialize ();


		public virtual bool validate() {
			return true;
		}

		public abstract void activate ();

		public virtual void activateLongPress() {
			showInformDynamic ();
		}	



		protected virtual void showInformDynamic() {
			var informBoard = (Board.boardList.Find (x => x is InformBoard) as InformBoard);
			if (string.IsNullOrEmpty (titleContent)) {
				informBoard.Close ();
			}  else {
				informBoard.Show (titleContent,textContent);
			}
		}


	}
}

