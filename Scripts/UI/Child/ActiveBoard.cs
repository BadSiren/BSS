using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.UI {
	public class ActiveBoard : UnitBoard
	{
		public List<Activable> acitvableList;

		private List<Activable> preActivableList = new List<Activable> ();
		private List<Activable> emptyList=new List<Activable> ();

		void Start() {
			emptyList.Capacity = 9;
		}

		public override void setSelectUnit(BaseUnit unit) {
			base.setSelectUnit (unit);
			if (selectUnit != null) {
				if (selectUnit.team == BSS.Unit.UnitTeam.Red) {
					setActivableList (selectUnit.activableList);
				} else {
					resetActivableList ();
				}
			} else {
				resetActivableList ();
			}
		}
		public void setSelectUnit(GameObject obj) {
			var _unit=obj.GetComponent<BaseUnit> ();
			setSelectUnit (_unit);
		}
		public void setSelectUnitInMine(GameObject obj) {
			var _unit=obj.GetComponent<BaseUnit> ();
			if (_unit.isMine) {
				setSelectUnit (_unit);
			}
		}

		public void setActivableList (List<Activable> _acitvableList) {
			preActivableList = acitvableList;
			acitvableList = _acitvableList;
			acitvableList.Capacity = 9;
			for (int i = 0; i < acitvableList.Capacity; i++) {
				if (i < acitvableList.Count) {
					sendToReceiver ("Button" + i.ToString (), acitvableList [i].icon);
					sendToReceiver ("ButtonInfo" + i.ToString (), acitvableList [i].infoContent);
				} else {
					sendBoolToReceiver ("Button" + i.ToString (), false);
					sendBoolToReceiver ("ButtonInfo" + i.ToString (), false);
				}
			}
		}
		public void resetActivableList () {
			preActivableList = emptyList;
			acitvableList = emptyList;
			for (int i = 0; i < acitvableList.Capacity; i++) {
				sendBoolToReceiver ("Button" + i.ToString (), false);
				sendBoolToReceiver ("ButtonInfo" + i.ToString (), false);
			}
		}
		public void undoActivableList () {
			setActivableList (preActivableList);
		}

		public virtual void activeButton(int _num) {
			if (acitvableList == null || acitvableList.Count - 1 < _num) {
				return;
			}
			acitvableList [_num].activate ();

		}

		public virtual void activeButtonLongPress(int _num) {
			if (acitvableList == null || acitvableList.Count - 1 < _num) {
				return;
			}
			acitvableList [_num].activateLongPress ();
		}
	}
}

