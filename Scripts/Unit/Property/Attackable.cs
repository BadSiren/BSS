using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public class AttackInfo {
		public GameObject attacker;
		public GameObject hitter;
		public float damage;
		public float attackSpeed;
	}
	public enum AttackType {
		Short,Long
	}
	[RequireComponent (typeof (BaseUnit))]
	public class Attackable : MonoBehaviour
	{
		private const float SIGHT=6f;
		private const float RANGETYPE=6f;

		public static List<Attackable> attackableList = new List<Attackable>();

		[HideInInspector]
		public AttackType attackType;

		[SerializeField]
		private float _initDamage;
		public float initDamage {
			get {
				return _initDamage;
			}
			private set {
				_initDamage = value;
			}
		}
		[SerializeField]
		private float _initAttackSpeed;
		public float initAttackSpeed {
			get {
				return _initAttackSpeed;
			}
			private set {
				_initAttackSpeed = value;
			}
		}
		[SerializeField]
		private float _initRange;
		public float initRange {
			get {
				return _initRange;
			}
			private set {
				_initRange = value;
			}
		}

		[HideInInspector]
		public float changeDamage=0f;
		public float damage {
			get {
				return initDamage+changeDamage;
			}
		}
		[HideInInspector]
		public float changeAttackSpeed=0f;
		public float attackSpeed {
			get {
				return initAttackSpeed+changeAttackSpeed;
			}
		}
		[HideInInspector]
		public float changeRange=0f;
		public float range {
			get {
				return initRange+changeRange;
			}
		}

		public bool isAttackable=true;
		private BaseUnit owner;
		private GameObject target;

		private MessageArgsTwo args;

		void Awake() {
			owner = GetComponent<BaseUnit> ();
			attackableList.Add(this);
		}

		void OnDestroy()
		{
			attackableList.Remove(this);
		}

		public void attack(GameObject enemyObject) {
			AttackInfo attackInfo=new AttackInfo ();
			attackInfo.attacker = gameObject;
			attackInfo.hitter = enemyObject;
			attackInfo.damage = damage;
			attackInfo.attackSpeed = attackSpeed;

			SendMessage ("onAttackEvent", attackInfo,SendMessageOptions.DontRequireReceiver);
			enemyObject.SendMessage ("onHitEvent",attackInfo, SendMessageOptions.DontRequireReceiver);
		}
		public GameObject findEnemy(float _range) {
			var unit=BaseUnit.unitList.Find (x => !(x.isInvincible) &&
				gameObject.GetInstanceID()!=x.gameObject.GetInstanceID() && checkHostile(owner.team,x.team)
				&& checkRange(x.transform.localPosition,_range) 
			);
			if (unit == null) {
				return null;
			} 
			return unit.gameObject;
		}
			

		private bool checkRange(Vector3 targetPos,float dis) {
			return Vector3.Distance (transform.localPosition, targetPos) < dis;
		}
		private bool checkHostile(UnitTeam team,UnitTeam other) {
			if ((team == UnitTeam.White || other == UnitTeam.White) || team == other) {
				return false;
			} 
			return true;
		}

		IEnumerator attackLoop() {
			while (true) {
				yield return new WaitForSeconds (1f/attackSpeed);
				if (target == null) {
					target = findEnemy (range);
					if (target == null) {
						target=findEnemy (range+SIGHT);
					}
				}
				if (target != null && isAttackable) {
					if (checkRange (target.transform.localPosition,range)) {
						attack (target);
					} else {
						GameObject nextTarget=findEnemy (range);
						if (findEnemy (range) == null) {
							if (checkRange (target.transform.localPosition, range + SIGHT)) {
								args.parameter0 = target;
								args.parameter1 = range * 0.9f;
								SendMessage ("toMove", args, SendMessageOptions.DontRequireReceiver);
							} else {
								target = null;
							}
						} else {
							target = nextTarget;
							attack (target);
						}
					}
				}
			}
		}

		private void onInitialize() {
			if (initRange > RANGETYPE) {
				attackType = AttackType.Long;
			} else {
				attackType = AttackType.Short;
			}
			StartCoroutine(attackLoop());
		}

		//UnitEvent
		private void onToMoveEvent(Vector3 targetPos) {
			isAttackable = false;
		}
		private void onMoveStopEvent() {
			isAttackable = true;
		}
		/*
		protected virtual void onHitEvent(AttackInfo attackInfo) {
			if (target == null) {
				args.parameter0 = attackInfo.attacker;
				args.parameter1 = range * 0.9f;
				SendMessage ("toMove", args, SendMessageOptions.DontRequireReceiver);
			}
		}
		*/



	}
}
