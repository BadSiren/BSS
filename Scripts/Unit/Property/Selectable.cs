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
		[HideInInspector]
		public BaseUnit owner;
		public bool isSelected {
			get {
				return BaseSelect.instance.selectObjects.Contains (gameObject);
			}
		}

		public void Awake()
		{
			owner = GetComponent<BaseUnit> ();
			selectableList.Add(this);
		}
		void OnDestroy()
		{
			selectableList.Remove(this);
		}
		void OnGUI() {
			if (isSelected) {
				Vector2 screenPoint1 = Camera.main.WorldToScreenPoint (new Vector2(transform.position.x-1f, transform.position.y-0.2f-0.5f));
				Vector2 screenPoint2 = Camera.main.WorldToScreenPoint (new Vector2(transform.position.x+1f, transform.position.y-0.2f+0.5f));
				var rect=DrawUtils.GetScreenRect (screenPoint1, screenPoint2);
				GUI.DrawTexture (rect, BaseSelect.instance.selectCircle);
			}
		}
			
		public override void onClick() {
			base.onClick ();
			onSelect ();
		}
		public override void onDoubleClick() {
			base.onDoubleClick ();
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

