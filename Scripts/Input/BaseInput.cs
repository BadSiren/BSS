using UnityEngine;
using System.Collections;
using BSS;
using System.Collections.Generic;
using UnityEngine.EventSystems;


namespace BSS.Input {
	public class BaseInput : MonoBehaviour
	{
		public Vector3 mousePoint;
		public float longPressTime=0.6f;
		public float doubleInterval=0.4f;

		public bool canCameraControl = true;
		public float cameraSpeed = 40f;


		private CameraControl cameraControl;

		private float pressTime=0f;
		private bool isLongPress;
		private bool isTouching;

		public void Awake()
		{
			if (canCameraControl) {
				cameraControl = Camera.main.gameObject.AddComponent<CameraControl> ();
				cameraControl.moveSpeed = cameraSpeed;
				cameraControl.enabled = false;
			}
		}
		void Update() {
			mouseInputWait ();
		}

		public void CameraControlEnabled(bool _enabled) {
			if (cameraControl == null) {
				return;
			}
			cameraControl.enabled = _enabled;
		}
		public static Vector3 getMousePoint2D() {
			var _mousePos=Camera.main.ScreenToWorldPoint (UnityEngine.Input.mousePosition);
			_mousePos=new Vector3 (_mousePos.x, _mousePos.y, 0f);
			return _mousePos;
		}


		private void mouseInputWait() {
			
			if (EventSystem.current.IsPointerOverGameObject()==true || (cameraControl!=null&& cameraControl.enabled==true)) {
				//Click UI or Camera Controling
				return;
			}
			if (UnityEngine.Input.GetMouseButtonDown(0)) {
				pressTime=0f;
			}

			if (UnityEngine.Input.GetMouseButton (0)) {
				pressTime += Time.deltaTime;
				if (!isLongPress && pressTime > longPressTime) {
					currentMousePointSave ();
					Clickable clickable = getClickPriority (mousePoint);
					if (clickable!=null) {
						clickable.onLongClick ();
					}
					isLongPress = true;
					pressTime = 0f;
					return;
				}
			}
			if (UnityEngine.Input.GetMouseButtonUp (0)) {
				if (isLongPress) {
					isLongPress = false;
					return;
				}
				currentMousePointSave ();
				Clickable click = getClickPriority (mousePoint);
				if (click == null) {return;}
				if (isTouching) {
					click.onClick ();
					click.onDoubleClick ();
				} else {
					StartCoroutine (waitTouch ());
					click.onClick ();
				}
			}
		}

		IEnumerator waitTouch() {
			isTouching = true;
			yield return new WaitForSeconds (doubleInterval);
			isTouching = false;
		}

		private void currentMousePointSave() {
			mousePoint = getMousePoint2D ();
		}
		private Clickable getClickPriority(Vector3 orgin) {
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
