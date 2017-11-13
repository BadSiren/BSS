using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	//Attackable Child Class
	public class AttackCollider : MonoBehaviour
	{
		public Attackable attakable;
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
			attakable.OnTriggerEnterTarget (col);
		}
		void OnTriggerExit2D(Collider2D col) {
			attakable.OnTriggerExitTarget (col);
		}
	}
}

