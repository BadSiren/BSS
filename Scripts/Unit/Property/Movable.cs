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

		public bool isMoving;
		private BaseUnit owner;
		public Vector3 destination;
		public bool isPause;

		private bool isIgnore;


		void Awake() {
			owner = GetComponent<BaseUnit> ();
			movableList.Add(this);

			initSpeed = speed;
		}
		void OnDestroy()
		{
			movableList.Remove(this);
		}

		void Update() {
			if (!isPause &&!checkZero (destination)) {
				if (checkDestination (destination)) {
					isIgnore = false;
					destination = Vector3.zero;
					SendMessage ("onMoveStopEvent", SendMessageOptions.DontRequireReceiver);
					SendMessage ("onArriveEvent", SendMessageOptions.DontRequireReceiver);
					return;
				}
				transform.position = Vector3.MoveTowards (transform.position, destination, speed * Time.deltaTime);
				if (!isMoving) {
					isMoving = true;
					SendMessage ("onAllMoveEvent",destination, SendMessageOptions.DontRequireReceiver);
				}
				return;
			}
			if (isMoving) {
				isMoving = false;
				SendMessage ("onMoveStopEvent", SendMessageOptions.DontRequireReceiver);
			}
		}


		public void toMove(Vector3 targetPos) {
			if (isIgnore) {
				return;
			}
			isPause = false;
			destination = targetPos;
			isMoving = true;
			if (targetPos != Vector3.zero) {
				SendMessage ("onAllMoveEvent", destination, SendMessageOptions.DontRequireReceiver);
			}
		}
		public void toMoveByForce(Vector3 targetPos) {
			isIgnore = true;
			isPause = false;
			destination = targetPos;
			isMoving = true;
			SendMessage ("onAllMoveEvent",destination, SendMessageOptions.DontRequireReceiver);
			SendMessage ("onMoveByForceEvent",destination, SendMessageOptions.DontRequireReceiver);
		}

		public void moveStopTimer(float _time) {
			StartCoroutine (coMoveStopTimer(_time));
		}
		IEnumerator coMoveStopTimer(float _time) {
			yield return new WaitForSeconds (_time);
			toMove (Vector3.zero);
		}
		public void movePause(bool reset=false) {
			if (isPause) {
				return;
			}
			isPause = true;
			if (reset) {
				StartCoroutine (coMoveResume ());
			}
		}
		public void moveResume() {
			if (!isPause) {
				return;
			}
			isPause = false;
		}


		IEnumerator coMoveResume(float _time=0.5f) {
			yield return new WaitForSeconds (_time);
			moveResume ();
		}
			
		private bool checkDestination(Vector3 des,float dis=0.5f) {
			return Vector3.Distance (transform.position, des) < dis;
		}
		private bool checkZero(Vector3 des) {
			return des == Vector3.zero;
		}
		private bool checkZero(Vector3 des0,Vector3 des1) {
			return (des0 == Vector3.zero && des1==Vector3.zero);
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
		private void onAttackEvent(AttackInfo attackInfo) {
			movePause (true);
		}
	}
}

