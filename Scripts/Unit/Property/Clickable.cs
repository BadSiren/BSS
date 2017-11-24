using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace BSS {
	[RequireComponent(typeof(Collider2D))]
	public class Clickable : SerializedMonoBehaviour
	{
		public int priority;
		public UnityEvent onClickEvent;
		public UnityEvent onDoubleClickEvent;
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

