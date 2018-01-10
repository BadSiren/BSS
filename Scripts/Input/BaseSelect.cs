using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS {	
	public class BaseSelect : SerializedMonoBehaviour
	{
		public Texture2D selectCircle;
		public static BaseSelect instance;
		public ESelectState eSelectState=ESelectState.None;
		public List<Selectable> selectableList=new List<Selectable> ();
		public bool isAttack {
			get;set;
		}

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
			BaseEventListener.onPublishGameObject ("UnitSelect", selectable.gameObject);
		}
		public void multiUnitSelect(List<Selectable> selectables) {
			eSelectState = ESelectState.Multi;
			selectableList.Clear ();
			selectableList = selectables;
			BaseEventListener.onPublish ("SelectCancle");
		}
		public void selectCancle() {
			eSelectState = ESelectState.None;
			selectableList.Clear ();
			BaseEventListener.onPublish ("SelectCancle");
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


	}
}

