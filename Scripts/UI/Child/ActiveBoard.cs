using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.UI {
	public class ActiveBoard : UnitBoard
	{
		public const int MAX_COUNT = 9;
		public List<Activable> acitvableList=new List<Activable> ();

		public override void changeSelectUnit (BaseUnit unit)
		{
			selectUnit = unit;
			changeCategory ("Base");
		}
		public override void clearSelectUnit ()
		{
			selectUnit = null;
			for (int i = 0; i < MAX_COUNT; i++) {
				clearActButtonImage (i);
			}
		}
		public void changeCategory(string category) {
			acitvableList=selectUnit.getActivableList ().FindAll (x => x.category == category);
			for (int i=0;i<acitvableList.Count;i++){
				setActButtonImage (i, acitvableList [i].icon, acitvableList [i].titleContent);
			}
		}

		public void activeButton(int _num) {
			if (acitvableList == null || acitvableList.Count - 1 < _num) {
				return;
			}
			acitvableList [_num].activate ();
		}

		public void activeButtonLongPress(int _num) {
			if (acitvableList == null || acitvableList.Count - 1 < _num) {
				return;
			}
			acitvableList [_num].activateLongPress ();
		}

		private void setActButtonImage(int index,Sprite icon,string title) {
			sendToReceiver ("ButtonTitle" + index.ToString (), title);
			sendToReceiver ("ButtonIcon" + index.ToString (), icon);
		}
		private void clearActButtonImage(int index) {
			sendBoolToReceiver ("ButtonTitle" + index.ToString (), false);
			sendBoolToReceiver ("ButtonIcon" + index.ToString (), false);
		}

		/*
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
		*/
	}
}

