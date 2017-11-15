using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	//Attackable Child Class
	public class AttackCollider : MonoBehaviour
	{
		public Attackable attakable;
		public CircleCollider2D circleCol;

		public string triggerEnter;
		public string triggerExit;

		void Awake ()
		{
			circleCol=gameObject.AddComponent<CircleCollider2D> ();
			circleCol.isTrigger = true;
		}
			
		public void setEnable(float _range) {
			gameObject.SetActive (true);
			setRadius (_range);
		}
		public void setDisable() {
			setRadius (0f);
			gameObject.SetActive (false);
		}
		public void setRadius(float size) {
			circleCol.radius = size;
		}
			
		void OnTriggerEnter2D(Collider2D col) {
			if (col.tag == "Ignore"||col is CircleCollider2D) {
				return;
			}
			BaseUnit unit =col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || unit.isInvincible) {return;}

			attakable.SendMessage (triggerEnter,col,SendMessageOptions.DontRequireReceiver);
		}
		void OnTriggerExit2D(Collider2D col) {
			if (col.tag == "Ignore"||col is CircleCollider2D) {
				return;
			}
			BaseUnit unit =col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || unit.isInvincible) {return;}

			attakable.SendMessage (triggerExit,col,SendMessageOptions.DontRequireReceiver);
		}
	}
}

