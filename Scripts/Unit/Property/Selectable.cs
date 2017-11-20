using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventsPlus;
using UnityEngine.UI;
using BSS.Unit;

namespace BSS {
	[RequireComponent(typeof(BaseUnit))]
	public class Selectable : Clickable
	{
		public static UnitTeam selectTeam;
		public static List<Selectable> selectableList = new List<Selectable>();
		public static List<GameObject> selectedList = new List<GameObject> ();
		public static GameObject lastSelected;

		public bool isSelected;
		public bool canInputing=true;
		private BaseUnit owner;

		public void Awake()
		{
			selectableList.Add (this);
			owner = GetComponent<BaseUnit> ();
		}
		public void OnDestroy()
		{
			selectableList.Remove (this);
		}
		public override void onClick() {
			base.onClick();
			selectedList.Clear ();
			onSelect ();
		}

		public void onSelect() {
			if (!canInputing) {
				return;
			}
			if (!selectedList.Contains (gameObject)) {
				selectedList.Add (gameObject);
			}
			lastSelected = gameObject;
			selectTeam = owner.team;
			isSelected = true;
			unSelectAllExceptMe ();
			SendMessage ("onSelectEvent", SendMessageOptions.DontRequireReceiver);
			BaseEventListener.onPublishGameObject ("UnitSelect", gameObject);
		}
		public void unSelect() {
			if (selectedList.Contains (gameObject)) {
				selectedList.Remove(gameObject);
			}
			isSelected = false;
			SendMessage ("onUnSelectEvent", SendMessageOptions.DontRequireReceiver);
		}
		public void unSelectAllExceptMe() {
			foreach (var it in selectableList) {
				if (gameObject.GetInstanceID () == it.gameObject.GetInstanceID ()) {
					continue;
				}
				it.unSelect ();
			}
		}
	}
}

