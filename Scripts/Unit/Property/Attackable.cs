using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

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
	public class Attackable : SerializedMonoBehaviour
	{
		public static List<Attackable> attackableList = new List<Attackable>();

		public Sprite icon;
		public UnitTeam team {
			get {return owner.team;}
		}

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
		private float _changeRange=0f;
		public float changeRange {
			get {
				return _changeRange;
			}
			set {
				_changeRange=value;
			}
		}
		public float range {
			get {
				return initRange+changeRange;
			}
		}
			
		public bool isOffenssive=true;
		public float offenseRange=15f;
		public bool isCounterattack = true;
		public float outRange=20f;
			

		private BaseUnit owner;
		public BaseUnit target {
			private set;
			get;
		}

		private Movable movable;
		[SerializeField]
		private bool isMoving {
			get{
				if (movable == null) {
					return false;
				}
				return movable.isMoving;
			}
		}



		void Awake() {
			owner = GetComponent<BaseUnit> ();
			movable = GetComponent<Movable> ();
			attackableList.Add(this);

			StartCoroutine (coDetectEnemy ());
			StartCoroutine (coAttackLoop ());
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

		public void setTarget(BaseUnit enemy){
			target = enemy;
		}

		public bool reFindTarget(float radius){
			var targets = UnitUtils.GetUnitsInCircle (transform.position, radius).FindAll (x => UnitUtils.CheckHostile (owner, x)&& !x.isInvincible);
			if (targets.Count == 0) {
				return false;
			}
			target = targets [0];
			return true;
		}

		IEnumerator coDetectEnemy() {
			while (true) {
				yield return new WaitForSeconds (0.1f);
				if (isMoving || target!=null) {
					continue;
				}
				if (!reFindTarget (range)) {
					if (isOffenssive && reFindTarget (offenseRange)) {
						SendMessage ("onDetectInOffenceRange", this, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
		IEnumerator coAttackLoop() {
			while (true) {
				yield return new WaitForSeconds (1f/attackSpeed);
				if (isMoving  || target == null) {
					continue;
				}
				if (checkRange (target)) {
					attack (target.gameObject);
				} else if (checkRange (target, outRange)) {
					target = null;
				}
			}
		}
			

		private bool checkRange(BaseUnit unit,float dis) {
			return UnitUtils.IsUnitInCircle (unit, transform.position, dis);
		}
		private bool checkRange(BaseUnit unit) {
			return UnitUtils.IsUnitInCircle (unit, transform.position, range);
		}

		//UnitEvent
		/*
		private void onHitEvent(AttackInfo attackInfo) {
			if (target == null && isCounterattack) {
				setTarget (attackInfo.attacker);
			}
		}

		private void onMoveByForceEvent() {
			isIgnore=true;
		}
		private void onMoveStopEvent() {
			isIgnore=false;
		}
		*/
	}
}
