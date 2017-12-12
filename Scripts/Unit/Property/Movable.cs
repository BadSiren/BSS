using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[RequireComponent (typeof (PolyNavAgent))]
	[RequireComponent (typeof (BaseUnit))]
	public class Movable : SerializedMonoBehaviour
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

		public bool isMoving { 
			get {
				return navAgent.hasPath;
			}
		}
		private BaseUnit owner;
		private PolyNavAgent navAgent;
		private bool isChase;

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
			if (isMoving) {
				return;
			}
			SendMessage ("onAllMoveEvent",targetPos, SendMessageOptions.DontRequireReceiver);
			navAgent.SetDestination (targetPos,(x)=> {
				moveStop();
			});
		}
		public void toMoveTarget(GameObject target,float distance) {
			if (isMoving) {
				return;
			}
			toMove (target.transform.position);
			StartCoroutine (coChaseTarget (target, distance));
		}


		public void toMoveByForce(Vector3 targetPos) {
			SendMessage ("onAllMoveEvent",targetPos, SendMessageOptions.DontRequireReceiver);
			navAgent.SetDestination (targetPos,(x)=> {
				moveStop();
			});
		}
		public void toMoveTargetByForce(GameObject target,float distance) {
			toMoveByForce (target.transform.position);
			StartCoroutine (coChaseTarget (target, distance));
		}

		public void toPatrol(Vector3 targetPos,float distance) {
			if (isMoving) {
				return;
			}
			toMove (targetPos);
			StartCoroutine (coFindTarget (distance));
		}

		public void moveStop() {
			isChase = false;
			navAgent.Stop ();
			SendMessage ("onMoveStopEvent", SendMessageOptions.DontRequireReceiver);
		}

		IEnumerator coChaseTarget(GameObject target,float distance) {
			isChase = true;
			while (isChase) {
				if (target == null) {
					moveStop ();
					continue;
				}
				if (UnitUtils.IsObjectInCircle (target,transform.position,distance)) {
					moveStop ();
					continue;
				}
				yield return new WaitForSeconds (0.1f);
			}
		}
		IEnumerator coFindTarget(float distance) {
			var isFind = true;
			while (isFind) {
				var enemies = UnitUtils.GetEnemiesInCircle (transform.position, distance, owner.team).FindAll (x => !x.isInvincible);
				if (enemies.Count>0) {
					isFind = false;
					moveStop ();
					continue;
				}
				yield return new WaitForSeconds (0.1f);
			}
		}
			
		//UnitEvent
		/*
		private void onAttackEvent(AttackInfo attackInfo) {
			moveStop ();
		}
		*/
		private void onDetectInOffenceRange(Attackable attakable) {
			toMoveTarget (attakable.target.gameObject, attakable.range);
		}

	}
}

