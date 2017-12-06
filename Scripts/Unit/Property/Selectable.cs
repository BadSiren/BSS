using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using BSS.Unit;

namespace BSS {
	[RequireComponent(typeof(BaseUnit))]
	public class Selectable : Clickable
	{
		public static List<Selectable> selectableList = new List<Selectable>();

		public BaseUnit owner;

		public void Awake()
		{
			owner = GetComponent<BaseUnit> ();
			selectableList.Add(this);
		}
		void OnDestroy()
		{
			selectableList.Remove(this);
		}
			
		public override void onClick() {
			base.onClick ();
			onSelect ();
		}
		public void onSelect() {
			if (owner.team == UnitTeam.Red) {
				BaseSelect.instance.allyUnitSelect (gameObject);
			} else if  (owner.team == UnitTeam.Blue) {
				BaseSelect.instance.enemyUnitSelect (gameObject);
			}
		}


		private void onDieEvent()
		{
			BaseSelect.instance.unitUnSelect (gameObject);
		}
		/*
		public void unSelect() {
			if (selectedList.Contains (gameObject)) {
				selectedList.Remove(gameObject);
			}
			isSelected = false;
			SendMessage ("onUnSelectEvent", SendMessageOptions.DontRequireReceiver);
		}
		public void unSelectAllExceptMe() {
			foreach (var it in selectableList) {
				if (it ==null || gameObject.GetInstanceID () == it.gameObject.GetInstanceID ()) {
					continue;
				}
				it.unSelect ();
			}
		}


		private void onDieEvent()
		{
			selectableList.Remove (this);
			selectedList.Remove (gameObject);
			if (lastSelected!=null &&lastSelected.GetInstanceID () == gameObject.GetInstanceID ()) {
				lastSelected = null;
				BaseEventListener.onPublish ("SelectCancle");
			}
		}
		*/
	}
}

