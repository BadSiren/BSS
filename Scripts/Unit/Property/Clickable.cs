using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace BSS {
	[RequireComponent(typeof(Collider2D))]
	public class Clickable : SerializedMonoBehaviour
	{
		public enum EClickType
		{
			Once,Double
		}
		public bool hasParent;
		public int priority;

		[FoldoutGroup("ClickEvent")]
		public List<Condition> needConditions=new List<Condition>();
		[FoldoutGroup("ClickEvent")]
		public List<Condition> unlessConditions=new List<Condition>();

		[FoldoutGroup("ClickEvent")]
		public EClickType eClickType;
		[FoldoutGroup("ClickEvent")]
		public UnityEvent onClickTrueEvent;
		[FoldoutGroup("ClickEvent")]
		public UnityEvent onClickFalseEvent;

		public virtual void onClick() {
			GameObject obj = gameObject;
			if (hasParent) {
				obj=transform.parent.gameObject;
			}
			foreach (var it in needConditions) {
				if (!Conditions.instance.validate (it,obj)) {
					onClickFalseEvent.Invoke ();
					return;
				}
			}
			foreach (var it in unlessConditions) {
				if (Conditions.instance.validate (it,obj)) {
					onClickFalseEvent.Invoke ();
					return;
				}
			}
			onClickTrueEvent.Invoke ();
		}

	}
}

