using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS {
	[RequireComponent(typeof(BaseUnit))]
    public class Selectable : MonoBehaviour,IHitReact,IDieReact,IItemUpdateReact,IActivableUpdateReact
	{
        public static List<Selectable> selectableList = new List<Selectable>();

		[HideInInspector]
		public BaseUnit owner;
        public bool isSelect {
            get {
                return BaseSelect.instance.selectableList.Contains(this);
            }
        }
		public bool isMainSelect {
			get {
                return BaseSelect.instance.mainSelectable == this;
			}
		}

		void Awake()
		{
			owner = GetComponent<BaseUnit> ();
			selectableList.Add(this);
		}
        void OnDestroy()
		{
            onDeselect();
			selectableList.Remove(this);
		}

		public void onSelect() {
			BaseSelect.instance.unitSelect (this);
		}
		public void onDeselect() {
			BaseSelect.instance.selectRemove (this);
		}

        public void onHit() {
            if (!isMainSelect) {
                return;
            }
            BaseEventListener.onPublishGameObject("Select"+owner.hitEvent, gameObject, gameObject);
        }

        public void onDie() {
            if (!isMainSelect) {
                return;
            }
            BaseEventListener.onPublishGameObject("Select" + owner.destroyEvent, gameObject, gameObject);
        }

        public void onItemUpdate() {
            if (!isMainSelect) {
                return;
            }
            BaseEventListener.onPublishGameObject("SelectItemUpdate", gameObject, gameObject);
        }

        public void onActivableUpdate() {
            if (!isMainSelect) {
                return;
            }
            BaseEventListener.onPublishGameObject("SelectActivableUpdate", gameObject, gameObject);
        }
    }
}

