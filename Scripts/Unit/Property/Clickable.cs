using UnityEngine;
using System.Collections;

namespace BSS {
	[RequireComponent(typeof(Collider2D))]
	public class Clickable : MonoBehaviour
	{
		public int priority;

		public virtual void onClick() {
			SendMessage ("onClickEvent",SendMessageOptions.DontRequireReceiver);
		}
		public virtual void onDoubleClick() {
			SendMessage ("onDoubleClickEvent",SendMessageOptions.DontRequireReceiver);
		}
		public virtual void onLongClick() {
			SendMessage ("onLongClickEvent",SendMessageOptions.DontRequireReceiver);
		}
	}
}

