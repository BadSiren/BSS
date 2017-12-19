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
		public int priority;

		[FoldoutGroup("ClickEvent")]
		public UnityEvent onClickEvent;
		[FoldoutGroup("ClickEvent")]
		public UnityEvent onDoubleClickEvent;
		[FoldoutGroup("ClickEvent")]
		public UnityEvent onLongClickEvent;

		public virtual void onClick() {
			onClickEvent.Invoke ();
		}
		public virtual void onDoubleClick() {
			onDoubleClickEvent.Invoke ();
		}
		public virtual void onLongClick() {
			onLongClickEvent.Invoke ();
		}
	}
}

