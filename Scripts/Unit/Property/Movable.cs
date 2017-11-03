using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using EventsPlus;

namespace BSS.Unit {
	[RequireComponent (typeof (BaseUnit))]
	public class Movable : MonoBehaviour
	{
		public static List<Movable> movableList = new List<Movable>();

		private float _initSpeed;
		public float initSpeed {
			get {
				return _initSpeed;
			}
			private set {
				_initSpeed = value;
			}
		}
		public float speed=10f;
		public bool canInputing;

		public bool moving { get ; set;}

		private BaseUnit owner;
		private Vector3 destination;
		private GameObject target;

		void Awake() {
			owner = GetComponent<BaseUnit> ();
			movableList.Add(this);

			initSpeed = speed;
		}
		void OnDestroy()
		{
			movableList.Remove(this);
		}


		public void toMove(Vector3 targetPos) {
			StopAllCoroutines ();
			target = null;
			destination = targetPos;
			moving = true;
			StartCoroutine (move(0.5f));
			SendMessage ("onToMoveEvent", destination,SendMessageOptions.DontRequireReceiver);
		}
		public void toMove(GameObject _target,float _range) {
			StopAllCoroutines ();
			target = _target;
			destination = target.transform.position;
			moving = true;
			StartCoroutine (move(_range));
			StartCoroutine (moveStopTimer(2f));
			SendMessage ("onToMoveEvent",destination,SendMessageOptions.DontRequireReceiver);
		}
		public void toMove(MessageArgsTwo args) {
			if (!moving &&args.parameter0 is GameObject && args.parameter1 is float) {
				toMove (args.parameter0 as GameObject, (float)args.parameter1);
			}
		}
		public void moveStop() {
			moving = false;
			target = null;
			SendMessage ("idleMotion", SendMessageOptions.DontRequireReceiver);
			SendMessage ("onMoveStopEvent",SendMessageOptions.DontRequireReceiver);
		}

		IEnumerator move(float distance) {
			while (moving) {
				if (checkDetination(distance)) {
					moveStop ();
					break;
				}
				if (target != null) {
					destination = target.transform.position;
				}
				transform.position = Vector3.MoveTowards (transform.position, destination, speed * Time.deltaTime);
				
				yield return null;
			}
			yield return null;
		}
		IEnumerator moveStopTimer(float time) {
			yield return new WaitForSeconds (time);
			moveStop ();
		}

		private bool checkDetination(float dis) {
			return Vector3.Distance (transform.position, destination) < dis;
		}
			

		//UnitEvent
		private void onSelectEvent() {
			if (owner.team == UnitTeam.Red) {
				canInputing = true;
			}
		}
		private void onUnSelectEvent() {
			canInputing = false;
		}

	}
}

