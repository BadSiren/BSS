using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[RequireComponent (typeof (PolyNavAgent))]
	[RequireComponent (typeof (BaseUnit))]
	public class Movable : SerializedMonoBehaviour,IItemPropertyApply
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
				navAgent.maxSpeed = speed;
			}
		}
		private float _changeSpeed=0f;
		public float changeSpeed {
			get {
				return _changeSpeed;
			}
			private set {
				_changeSpeed = value;
				navAgent.maxSpeed = speed;
			}
		}
		public float speed {
			get {
				return initSpeed + changeSpeed;
			}
		}

		public bool isMoving { 
			get {
				return navAgent.hasPath;
			}
		}
		[HideInInspector]
		public BaseUnit owner;
		private PolyNavAgent navAgent;
		private bool isChase;

		void Awake() {
			owner = GetComponent<BaseUnit> ();
			navAgent=GetComponent<PolyNavAgent> ();
			movableList.Add(this);
			navAgent.maxSpeed = speed;
		}
		void OnDestroy()
		{
			movableList.Remove(this);
		}



		public void toMove(Vector2 targetPos) {
			navAgent.SetDestination (targetPos);
		}
		public void toMove(Vector2 targetPos,System.Action stopAct) {
			navAgent.SetDestination (targetPos,(x)=> {
				stopAct();
			});
		}


		public void toMoveTarget(GameObject target,float distance) {
			if (isMoving) {
				return;
			}
			toMove (target.transform.position);
			StartCoroutine (coChaseTarget (target, distance));
		}


		public void toMoveByForce(Vector2 targetPos) {
			navAgent.SetDestination (targetPos,(x)=> {
				moveStop();
			});
		}
		public void toMoveTargetByForce(GameObject target,float distance) {
			toMoveByForce (target.transform.position);
			StartCoroutine (coChaseTarget (target, distance));
		}

		public void toPatrol(Vector2 targetPos,float distance) {
			if (isMoving) {
				return;
			}
			toMove (targetPos);
			StartCoroutine (coFindTarget (distance));
		}

		public void moveStop() {
			isChase = false;
			navAgent.Stop ();
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

		public void addProperty(string ID,float value) {
			if (ID != "MoveSpeed") {
				return;
			}
			changeSpeed += value;
		}
		public void removeProperty(string ID,float value) {
			if (ID != "MoveSpeed") {
				return;
			}
			changeSpeed -= value;
		}
			

	}
}

