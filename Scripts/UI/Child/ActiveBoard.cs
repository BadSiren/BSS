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

		public void setActivableList (List<Activable> _acitvableList) {
			preActivableList = acitvableList;
			acitvableList = _acitvableList;
			acitvableList.Capacity = 9;
			for (int i = 0; i < acitvableList.Capacity; i++) {
				if (i < acitvableList.Count) {
					acitvableList [i].onShow ();
					sendToReceiver ("Button" + i.ToString (), acitvableList [i].buttonImage);
				} else {
					sendBoolToReceiver ("Button" + i.ToString (), false);
				}
			}
		}
		public void resetActivableList () {
			preActivableList = emptyList;
			acitvableList = emptyList;
			for (int i = 0; i < acitvableList.Capacity; i++) {
				sendBoolToReceiver ("Button" + i.ToString (), false);
			}
		}
		public void undoActivableList () {
			setActivableList (preActivableList);
		}

		public virtual void activeButton(int _num) {
			if (acitvableList == null || acitvableList.Count - 1 < _num) {
				return;
			}
			acitvableList [_num].activate (selectUnit);

		}

		public virtual void activeButtonLongPress(int _num) {
			if (acitvableList == null || acitvableList.Count - 1 < _num) {
				return;
			}
			acitvableList [_num].activateLongPress (selectUnit);
		}
	}
}

