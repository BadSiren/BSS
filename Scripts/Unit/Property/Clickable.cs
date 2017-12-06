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

		[Header("None, AllySelect, EnemySelect, MultiSelect")]
		[Header("[StateKind]")]
		public List<string> onClickState = new List<string> ();
		public UnityEvent onClickEvent;
		public List<string> onDoubleClickState = new List<string> ();
		public UnityEvent onDoubleClickEvent;
		public List<string> onLongClickState = new List<string> ();
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

		public virtual bool isClick(string _state) {
			return isStateValidate (onClickState, _state);
		}
		public virtual bool isDoubleClick(string _state) {
			return isStateValidate (onDoubleClickState, _state);
		}
		public virtual bool isLongClick(string _state) {
			return isStateValidate (onLongClickState, _state);
		}

		private bool isStateValidate(List<string> contain,string _state) {
			if (contain.Count == 0 || contain.Contains (_state)) {
				return true;
			}
			return false;
		}

	}
}

