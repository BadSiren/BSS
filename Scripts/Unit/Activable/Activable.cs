using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BSS.Unit {
	[System.Serializable]
    public abstract class Activable : SerializedMonoBehaviour
	{	
		[FoldoutGroup("ActBase(Mandatory)")]
		public bool isPrivate;
        [FoldoutGroup("ActBase(Mandatory)")]
        public bool isIgnore;
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
                return activables.getIndex(this);
            }
        }
        private BaseUnit _owner;
        public BaseUnit owner {
            get {
                if (_owner == null) {
                    _owner = GetComponentInParent<BaseUnit>();
                }
                return _owner;
            }
        }
        private Activables _activables;
        public Activables activables {
            get {
                if (_activables == null) {
                    _activables = GetComponentInParent<Activables>();
                }
                return _activables;
            }
        }
        public bool isSelected {
            get {
                return activables.selectedAct == index;
            }
        }

        void Start() {
            initialize();
        }
        void OnDestroy() {
            deInitialize();
        }

        public abstract void initialize ();
        public virtual void deInitialize() { }

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

        public bool checkDisplayable() {
            if (!isPrivate) {
                return true;
            }
            return owner.onlyMine;
        }
        public bool checkInteractable() {
            if (!isIgnore) {
                return true;
            }
            return owner.onlyMine;
        }


    }
}

