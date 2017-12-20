using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BSS {
	[RequireComponent(typeof(Collider2D))]
	public class ClickObject : MonoBehaviour
	{
		public UnityEvent onClick = new UnityEvent();

		void Update() {
			if (UnityEngine.Input.GetMouseButtonDown(0)) {
				var mousePos=Camera.main.ScreenToWorldPoint (UnityEngine.Input.mousePosition);
				mousePos=new Vector3 (mousePos.x, mousePos.y, 0f);
				RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, 0f);
				foreach (var it in hits) {
					var temp = it.collider.gameObject.GetComponent<ClickObject> ();
					if (temp != null && temp == this) {
						onClick.Invoke ();
						break;
					}
				}
			}
		}

	}
}

