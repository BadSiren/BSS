using UnityEngine;
using System.Collections;
using BSS;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using BSS.Unit;

namespace BSS.Input {
	public class BaseInput : MonoBehaviour
	{
		private const int MAX_MULTI_SELECT=9;
		//public float longPressTime=0.6f;
		public float doubleInterval=0.4f;

		//private float pressTime=0f;
		private bool isDrag;
		private Vector3 preMousePoint;
		private bool isLongPress;
		private bool isTouching;


		void Update() {
			if(UnityEngine.Input.touchCount > 1){
				isDrag = false;
				//CameraControl
			} else {
				if (UnityEngine.Input.GetMouseButtonDown (0)) {
					//Cancle in UI Click
					if (EventSystem.current.IsPointerOverGameObject()) {
						return;
					}
					Vector3 currentMouse = getMousePoint2D ();
					Clickable click = getClickablePriorityOrNull (currentMouse);

					if (isTouching) {
						if (click != null) {
							//Double Click
							click.onDoubleClick ();
						}
						isTouching = false;
					} else {
						if (click != null) {
							//On Click
							click.onClick ();
							StartCoroutine (coWaitDoubleTouch ());
						}
					}
				}
			}
		}
			
		void OnGUI() {
			if (isDrag) {
				Vector3 curMousePoint = UnityEngine.Input.mousePosition;
				DrawUtils.DrawCustomRect (preMousePoint, curMousePoint);
			}
		}


		public static Vector3 getMousePoint2D() {
			var _mousePos=Camera.main.ScreenToWorldPoint (UnityEngine.Input.mousePosition);
			_mousePos = new Vector3 (_mousePos.x, _mousePos.y, 0f);
			return _mousePos;
		}

		public void setDrag() {
			if (isDrag) {
				return;
			}
			isDrag = true;
			preMousePoint = UnityEngine.Input.mousePosition;
			StartCoroutine (coWaitDrag ());
		}
		IEnumerator coWaitDrag() {
			while (isDrag) {
				yield return null;
				if (UnityEngine.Input.GetMouseButtonUp (0)) {
					isDrag = false;
					Vector3 curMousePoint = UnityEngine.Input.mousePosition;
					var bounds=DrawUtils.GetViewportBounds (Camera.main, preMousePoint, curMousePoint);
					List<GameObject> selectObjects = new List<GameObject> ();
					foreach (var it in Selectable.selectableList) {
						if (bounds.Contains (Camera.main.WorldToViewportPoint (it.gameObject.transform.position)) && it.owner.team==UnitTeam.Red ) {
							if (selectObjects.Count < MAX_MULTI_SELECT) {
								selectObjects.Add (it.gameObject);
							}
						}
					}
					if (selectObjects.Count == 1) {
						BaseSelect.instance.allyUnitSelect (selectObjects [0]);
					} else if (selectObjects.Count > 1) {
						BaseSelect.instance.multiUnitSelect (selectObjects);
					}
				}
			}
		}

		IEnumerator coWaitDoubleTouch() {
			isTouching = true;
			yield return new WaitForSeconds (doubleInterval);
			isTouching = false;
		}


		private Clickable getClickablePriorityOrNull(Vector3 orgin) {
			RaycastHit2D[] hits = Physics2D.RaycastAll(orgin, Vector2.zero, 0f);
			List<Clickable> clickableList = new List<Clickable> ();
			foreach (var it in hits) {
				if (it.collider.GetType()==typeof(CircleCollider2D)) {
					continue;
				}
				Clickable temp=it.collider.GetComponent<Clickable> ();
				if (temp != null) {
					clickableList.Add (temp);
				}
				clickableList.Sort ((t1, t2) => t2.priority - t1.priority);
			}
			if (clickableList.Count > 0) {
				return clickableList [0];
			}
			return null;
		}


	}
}
