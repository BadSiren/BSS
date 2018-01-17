using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;
using BSS.UI;

namespace BSS {	
	public class BaseSelect : SerializedMonoBehaviour
	{
		public Texture2D selectCircle;
		public static BaseSelect instance;
		public ESelectState eSelectState=ESelectState.None;
		public Selectable mainSelectable;
		public List<Selectable> selectableList=new List<Selectable> ();



		void Awake() {
			instance = this;
		}
		public void unitSelect(Selectable selectable) {
			if (selectable.owner.isMine) {
				eSelectState = ESelectState.Mine;
			} else {
				eSelectState = ESelectState.NotMine;
			}
			selectableList.Clear ();
			selectableList.Add(selectable);
			mainSelectable = selectable;
		}
		public void multiUnitSelect(List<Selectable> selectables) {
			eSelectState = ESelectState.Multi;
			selectableList.Clear ();
			selectableList = selectables;
			mainSelectable = null;
		}
		public void selectCancle() {
			eSelectState = ESelectState.None;
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

