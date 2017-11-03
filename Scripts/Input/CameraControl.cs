using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace BSS.Input {
	public class CameraControl : MonoBehaviour
	{
		public float moveSpeed;

		private Transform cam;
		private Vector3 prePos = Vector3.zero;

		// Use this for initialization
		void Awake ()
		{
			cam = Camera.main.transform;
		}

		void Update ()
		{
			cameraInputWait ();
		}

		private void cameraInputWait() {
			if (!UnityEngine.Input.GetMouseButton (0) || EventSystem.current.IsPointerOverGameObject()==true) {
				return;
			}
			if (UnityEngine.Input.GetMouseButtonDown (0)) {
				prePos=UnityEngine.Input.mousePosition;
			}
			if (prePos == Vector3.zero) {
				prePos=UnityEngine.Input.mousePosition;
				return;
			}
			Vector3 vec = (UnityEngine.Input.mousePosition - prePos).normalized;
			cam.position -= vec* moveSpeed * Time.deltaTime;
			cam.position = new Vector3 (cam.position.x, cam.position.y, -10f);
			prePos = UnityEngine.Input.mousePosition;
		}
	}
}

