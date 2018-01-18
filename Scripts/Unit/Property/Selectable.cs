using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using BSS.Unit;
using Sirenix.OdinInspector;

namespace BSS {
	[RequireComponent(typeof(BaseUnit))]
	public class Selectable : MonoBehaviour
	{
        public static List<Selectable> selectableList = new List<Selectable>();
		[BoxGroup("Event(GameObject)")]
		public string selectEvent="UnitSelect";

		[HideInInspector]
		public BaseUnit owner;
		public bool isSelected {
			get {
				return BaseSelect.instance.selectableList.Contains (this);
			}
		}

		public void Awake()
		{
			owner = GetComponent<BaseUnit> ();
			selectableList.Add(this);
		}
		void OnDestroy()
		{
			BaseSelect.instance.selectRemove (this);
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

		public void onSelect() {
			BaseSelect.instance.unitSelect (this);
			BaseEventListener.onPublishGameObject (selectEvent, owner.gameObject, owner.gameObject);
		}
		public void deSelect() {
			BaseSelect.instance.selectRemove (this);
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

