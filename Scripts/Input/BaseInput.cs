using UnityEngine;
using System.Collections;
using EventsPlus;
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
						clickable.onLongClick (mousePoint);
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
					click.onClick (mousePoint);
					click.onDoubleClick (mousePoint);
				} else {
					StartCoroutine (waitTouch ());
					click.onClick (mousePoint);
				}
			}
		}

		IEnumerator waitTouch() {
			isTouching = true;
			yield return new WaitForSeconds (doubleInterval);
			isTouching = false;
		}

		private void currentMousePointSave() {
			mousePoint=Camera.main.ScreenToWorldPoint (UnityEngine.Input.mousePosition);
			mousePoint = new Vector3 (mousePoint.x, mousePoint.y, 0f);
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
