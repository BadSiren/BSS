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
		private const float doubleInterval=0.2f;

		//private float pressTime=0f;
		private bool isDrag;
		private Vector3 preMousePoint;
		private bool isTouching;

		public static Vector2 getMousePoint() {
			var _mousePos=(Vector2)Camera.main.ScreenToWorldPoint (UnityEngine.Input.mousePosition);
			return _mousePos;
		}

		void Update() {
			if(UnityEngine.Input.touchCount > 1){
				isDrag = false;
				//CameraControl
			} else {
				if (UnityEngine.Input.GetMouseButtonDown (0)) {
					//Cancle in UI Click
					if (EventSystem.current.IsPointerOverGameObject () || isTouching) {
						return;
					}
					setDrag ();
					isTouching = true;
					StartCoroutine (coWaitDoubleTouch ());
				}
			}
		}
			
		void OnGUI() {
			if (isDrag) {
				Vector3 curMousePoint = UnityEngine.Input.mousePosition;
				DrawUtils.DrawCustomRect (preMousePoint, curMousePoint);
			}
		}

		public void onClickInMousePoint(Clickable.EClickType eClickType) {
			var clickableList = getClickableList ().FindAll (x => x.eClickType == eClickType);

			foreach (var it in clickableList) {
				it.onClick ();
			}
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
				if (!UnityEngine.Input.GetMouseButton (0)) {
					isDrag = false;
					Vector3 curMousePoint = UnityEngine.Input.mousePosition;
					var bounds=DrawUtils.GetViewportBounds (Camera.main, preMousePoint, curMousePoint);
					List<Selectable> selectableList = new List<Selectable> ();
					foreach (var it in Selectable.selectableList) {
						if (bounds.Contains (Camera.main.WorldToViewportPoint (it.gameObject.transform.position)) && it.owner.isMine) {
							if (selectableList.Count < MAX_MULTI_SELECT) {
								selectableList.Add (it);
							}
						}
					}
					if (selectableList.Count == 1) {
                        selectableList[0].onSelect();
					} else if (selectableList.Count > 1) {
						BaseSelect.instance.multiUnitSelect (selectableList);
					}
				}
			}
		}

		IEnumerator coWaitDoubleTouch() {
			yield return null;
			float lastTime = Time.time;
			while (isTouching) {
				if (UnityEngine.Input.GetMouseButtonDown (0)) {
					isTouching = false;
					onClickInMousePoint (Clickable.EClickType.Double);
					break;
				}
				if (Time.time - lastTime > doubleInterval) {
					isTouching = false;
					onClickInMousePoint (Clickable.EClickType.Once);
					break;
				}
				yield return null;
			}
		}


		private List<Clickable> getClickableList() {
			RaycastHit2D[] hits = Physics2D.RaycastAll(getMousePoint(), Vector2.zero, 0f);
			List<Clickable> clickableList = new List<Clickable> ();
			foreach (var it in hits) {
				var clickables=it.collider.GetComponentsInChildren<Clickable> ();
				for (int i=0; i<clickables.Length;i++) {
					clickableList.Add (clickables[i]);
				}
			}
			return clickableList;
		}


	}
}
