using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;

namespace BSS.Unit {
	[RequireComponent (typeof (PolyNavAgent))]
	[RequireComponent (typeof (BaseUnit))]
	public class Movable : MonoBehaviour
	{
		public static List<Movable> movableList = new List<Movable>();

		[SerializeField]
		private float _initSpeed;
		public float initSpeed {
			get {
				return _initSpeed;
			}
			private set {
				_initSpeed = value;
			}
		}
		public float speed {
			get {
				return navAgent.maxSpeed;
			}
			set {
				navAgent.maxSpeed=value;
			}
		}
		public Vector3 destination;
		public bool isForce;

		private BaseUnit owner;
		private PolyNavAgent navAgent;



		void Awake() {
			owner = GetComponent<BaseUnit> ();
			navAgent=GetComponent<PolyNavAgent> ();
			movableList.Add(this);

			speed = initSpeed;
		}
		void OnDestroy()
		{
			movableList.Remove(this);
		}

		public void toMove(Vector3 targetPos) {
			if (isForce) {
				return;
			}
			SendMessage ("onAllMoveEvent",targetPos, SendMessageOptions.DontRequireReceiver);
			navAgent.SetDestination (targetPos,(x)=> {
				moveStop();
			});
		}
		public void toMove(Vector3 targetPos,float stopDistance) {
			if (isForce) {
				return;
			}
			navAgent.stoppingDistance = stopDistance;
			toMove (targetPos);
		}

		public void toMoveByForce(Vector3 targetPos) {
			isForce = true;
			SendMessage ("onAllMoveEvent",targetPos, SendMessageOptions.DontRequireReceiver);
			SendMessage ("onMoveByForceEvent",targetPos, SendMessageOptions.DontRequireReceiver);
			navAgent.SetDestination (targetPos,(x)=> {
				moveStop();
			});
		}
		public void toMoveByForce(Vector3 targetPos,float stopDistance) {
			navAgent.stoppingDistance = stopDistance;
			toMoveByForce (targetPos);
		}


		public void moveStop() {
			navAgent.Stop ();
			navAgent.stoppingDistance = 0.1f;
			SendMessage ("onMoveStopEvent", SendMessageOptions.DontRequireReceiver);
		}
			
			
		//UnitEvent
		private void onAttackEvent(AttackInfo attackInfo) {
			moveStop ();
		}
	}
}

