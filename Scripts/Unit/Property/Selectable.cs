using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventsPlus;
using UnityEngine.UI;

namespace BSS {
	public class Selectable : Clickable
	{
		public static List<Selectable> selectableList = new List<Selectable>();
		public static GameObject lastedSelect;
		public bool isSelected;
		public bool canInputing=true;

		public void Awake()
		{
			selectableList.Add (this);
		}
		public void OnDestroy()
		{
			selectableList.Remove (this);
		}
		public override void onClick(Vector3 mousePos) {
			base.onClick(mousePos);
			onSelect ();
		}

		public void onSelect() {
			if (!canInputing) {
				return;
			}
			lastedSelect = gameObject;
			isSelected = true;
			unSelectAllExceptMe ();
			SendMessage ("onSelectEvent", SendMessageOptions.DontRequireReceiver);
			BaseSelect.instance.setSelect (gameObject);
		}
		public void unSelect() {
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

