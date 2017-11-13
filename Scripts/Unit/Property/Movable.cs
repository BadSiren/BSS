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
		public bool isChasable=true;
		public bool isPause;
		public GameObject chaseTarget;


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
			if (isChasable &&chaseTarget != null) {
				if (checkDestination(chaseTarget.transform.localPosition)) {
					chaseTarget = null;
					return;
				}
				transform.position = Vector3.MoveTowards (transform.position, chaseTarget.transform.localPosition, speed * Time.deltaTime);
				if (!isMoving) {
					isMoving = true;
					SendMessage ("onAllMoveEvent",chaseTarget.transform.localPosition, SendMessageOptions.DontRequireReceiver);
				}
				return;
			}
				
			if (!isPause &&!checkZero (destination)) {
				if (checkDestination (destination)) {
					isChasable = true;
					chaseTarget = null;
					destination = Vector3.zero;
					SendMessage ("onMoveStopEvent",SendMessageOptions.DontRequireReceiver);
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
			isPause = false;
			isChasable = false;
			chaseTarget = null;
			destination = targetPos;
		}
		public void toPatrol(Vector3 targetPos) {
			isPause = false;
			isChasable = true;
			chaseTarget = null;
			destination = targetPos;
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


		//Enemy Chace AI
		private void onFirstReserveEvent(GameObject obj) {
			List<GameObject> targets = owner.getTargets ();
			if (targets != null && targets.Count == 0) {
				chaseTarget = obj;
			}
		}
		private void onEnterTargetEvent(GameObject obj) {
			if (chaseTarget!=null) {
				chaseTarget = null;
				isPause = true;
			}
		}
		private void onNothingTargetEvent() {
			List<GameObject> reserves = owner.getReserveTargets ();
			if (reserves != null && reserves.Count > 0) {
				chaseTarget = reserves [0];
			}
		}
		private void onNothingDetectedEvent() {
			chaseTarget = null;
			isPause = false;
		}
		/*
		private void onExitDetectedEvent(GameObject obj) {
			if (chaseTarget!=null && obj.GetInstanceID()==chaseTarget.GetInstanceID()) {
				List<GameObject> targets = owner.getTargets ();
				if (targets != null && targets.Count > 0) {
					chaseTarget = null;
					isPause = true;
				} else {
					if (owner.detectedUnits.Count > 0) {
						chaseTarget = owner.detectedUnits [0];
					} else {
						chaseTarget = null;
						isPause = false;
					}
				}
			}
		}
		*/


		/*
		private void onAttackEvent() {
			movePause (true);
		}
		private void onFindTargetEvent() {
			movePause ();
		}
		private void onExitTargetEvent() {
			moveResume ();
		}
		*/



	}
}

