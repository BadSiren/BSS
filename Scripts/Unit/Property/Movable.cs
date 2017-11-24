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
		public bool canInputing;

		public bool isMoving;
		private BaseUnit owner;
		public Vector3 destination;
		public bool isPause;

		private PolyNavAgent navAgent;
		private bool isIgnore;


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
			SendMessage ("onAllMoveEvent",targetPos, SendMessageOptions.DontRequireReceiver);
			navAgent.SetDestination (targetPos,(x)=> {
				SendMessage ("onMoveStopEvent", SendMessageOptions.DontRequireReceiver);
			});
			/*
			if (isIgnore) {
				return;
			}
			isPause = false;
			destination = targetPos;
			isMoving = true;
			if (targetPos != Vector3.zero) {
				SendMessage ("onAllMoveEvent", destination, SendMessageOptions.DontRequireReceiver);
			}
			*/
		}
		public void toMoveByForce(Vector3 targetPos) {
			SendMessage ("onAllMoveEvent",targetPos, SendMessageOptions.DontRequireReceiver);
			navAgent.SetDestination (targetPos,(x)=> {
				SendMessage ("onMoveStopEvent", SendMessageOptions.DontRequireReceiver);
			});
			/*
			isIgnore = true;
			isPause = false;
			destination = targetPos;
			isMoving = true;
			SendMessage ("onAllMoveEvent",destination, SendMessageOptions.DontRequireReceiver);
			SendMessage ("onMoveByForceEvent",destination, SendMessageOptions.DontRequireReceiver);
			*/
		}
		public void moveStop() {
			navAgent.Stop ();
			SendMessage ("onMoveStopEvent", SendMessageOptions.DontRequireReceiver);
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
		}
	}
}

