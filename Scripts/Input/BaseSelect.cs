using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;
using BSS.UI;

namespace BSS {
    public class BaseSelect : SerializedMonoBehaviour {
        public Texture2D selectCircle;
        public static BaseSelect instance;
        public ESelectState eSelectState = ESelectState.None;
        public ESelectUnitState eSelectUnitState = ESelectUnitState.None;
        public Selectable mainSelectable;
        public List<Selectable> selectableList = new List<Selectable>();
        [BoxGroup("Event(GameObject)")]
        public string selectEvent = "UnitSelect";
        [BoxGroup("Event(GameObject)")]
        public string deselectEvent = "UnitDeselect";


        void Awake() {
            instance = this;
        }
        public void setSelectUnitState(string state) {
            eSelectUnitState = (ESelectUnitState)System.Enum.Parse(typeof(ESelectUnitState), state);
        }

        public void unitSelect(Selectable selectable) {
            eSelectState = ESelectState.NotMine;
			if (selectable.owner.isMine) {
				eSelectState = ESelectState.Mine;
			}
            eSelectUnitState = ESelectUnitState.None;
            foreach (var it in selectableList) {
                BaseEventListener.onPublishGameObject(deselectEvent, it.gameObject, it.gameObject);
            }
                
			selectableList.Clear ();
			selectableList.Add(selectable);
            BaseEventListener.onPublishGameObject(selectEvent, selectable.gameObject, selectable.gameObject);
			mainSelectable = selectable;
		}
        public void multiUnitSelect(List<Selectable> selectables) {
			eSelectState = ESelectState.Multi;
            eSelectUnitState = ESelectUnitState.None;
            foreach (var it in selectableList) {
                BaseEventListener.onPublishGameObject(deselectEvent, it.gameObject, it.gameObject);
            }
			selectableList.Clear ();
			selectableList = selectables;
			mainSelectable = null;
		}
        public void selectCancle() {
			eSelectState = ESelectState.None;
            eSelectUnitState = ESelectUnitState.None;
            foreach (var it in selectableList) {
                BaseEventListener.onPublishGameObject(deselectEvent, it.gameObject, it.gameObject);
            }
			selectableList.Clear ();
			mainSelectable = null;
		}

		public void selectRemove(Selectable selectable) {
			if (!selectableList.Contains(selectable)) {
				return;
			}
			selectableList.Remove (selectable);
			if (selectableList.Count == 0) {
				selectCancle ();
			} else if (selectableList.Count == 1) {
				unitSelect (selectableList [0]);
			}
		}

		public bool isSelect(BaseUnit unit) {
			if (mainSelectable == null) {
				return false;
			}
			return mainSelectable.owner == unit;
		}



	}
}

