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
	public abstract class Activable : SerializedMonoBehaviour
	{
		[FoldoutGroup("ActBase(Mandatory)")]
		public string ID;
		[SerializeField]
		[FoldoutGroup("ActBase(Mandatory)")]
		private Sprite _icon;
		public virtual Sprite icon {
			get {
				return _icon;
			}
		}
		[FoldoutGroup("ActBase(Mandatory)")]
		[SerializeField]
		[TextArea()]
		private string _titleContent;
		public virtual string titleContent {
			get {
				return _titleContent;
			}
		}
		[FoldoutGroup("ActBase(Mandatory)")]
		[SerializeField]
		[TextArea()]
		private string _textContent;
		public virtual string textContent {
			get {
				return _textContent;
			}
		}
		public virtual string infoContent {
			get {
				return "";
			}
		}
		protected BaseUnit owner;

		void Awake() {
			owner = GetComponentInParent<BaseUnit> ();
			owner.activableList.Add (this);
		}
		void Start() {
			initialize ();
		}
		void OnDestroy() {
			if (owner != null && owner.activableList.Contains (this)) {
				owner.activableList.Remove (this);
			}
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

