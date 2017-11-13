using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	//BaseUnit Child Class
	public class DetectCollider : MonoBehaviour
	{
		public BaseUnit baseUnit;
		public CircleCollider2D circleCol;

		void Awake ()
		{
			circleCol=gameObject.AddComponent<CircleCollider2D> ();
			circleCol.isTrigger = true;
		}

		public void setRadius(float size) {
			circleCol.radius = size;
		}

		void OnTriggerEnter2D(Collider2D col) {
			baseUnit.OnTriggerEnterDetected (col);
		}
		void OnTriggerExit2D(Collider2D col) {
			baseUnit.OnTriggerExitDetected (col);
		}
	}
}

