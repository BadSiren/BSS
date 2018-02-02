using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;
using BSS.UI;

namespace BSS {
    public class BaseSelect : SerializedMonoBehaviour {
        public static BaseSelect instance;
        public Texture2D selectCircle;
        public Texture2D enemySelectCircle;

        public ESelectState eSelectState = ESelectState.None;
        public Selectable mainSelectable;
        public List<Selectable> selectableList = new List<Selectable>();
        [BoxGroup("Event(GameObject)")]
        public string selectEvent = "UnitSelect";
        [BoxGroup("Event(GameObject)")]
        public string deselectEvent = "UnitDeselect";


        void Awake() {
            instance = this;

        }
        void Start() {
        }

        public void unitSelect(Selectable selectable) {
            eSelectState = ESelectState.NotMine;
            if (selectable.owner.onlyMine) {
				eSelectState = ESelectState.Mine;
			}
            mainSelectable = selectable;
            foreach (var it in selectableList) {
                BaseEventListener.onPublishGameObject(deselectEvent, it.gameObject, it.gameObject);
            }
			selectableList.Clear ();
            selectableList.Add(selectable);
            BaseEventListener.onPublishGameObject(selectEvent, selectable.gameObject, selectable.gameObject);
			
		}
        public void multiUnitSelect(List<Selectable> selectables) {
			eSelectState = ESelectState.Multi;
            mainSelectable = null;
            foreach (var it in selectableList) {
                BaseEventListener.onPublishGameObject(deselectEvent, it.gameObject, it.gameObject);
            }
			selectableList.Clear ();
			selectableList = selectables;
			
		}
        public void selectCancle() {
			eSelectState = ESelectState.None;
            mainSelectable = null;
            foreach (var it in selectableList) {
                BaseEventListener.onPublishGameObject(deselectEvent, it.gameObject, it.gameObject);
            }
            selectableList.Clear();
		}

        public void selectRemove(Selectable selectable) {
            if (!selectableList.Contains(selectable)) {
                return;
            }
            selectableList.Remove(selectable);
            if (selectableList.Count == 0) {
                selectCancle();
            } else if (selectableList.Count == 1) {
                unitSelect(selectableList[0]);
            }
        }
        public bool isMainSelect(BaseUnit unit) {
            var selectable=unit.gameObject.GetComponent<Selectable>();
            if (mainSelectable == null || selectable == null) {
                return false;
            }
            return mainSelectable == selectable;
        }

	}
}

