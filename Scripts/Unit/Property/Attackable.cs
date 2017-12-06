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

		public bool isChase=true;
		public bool isCounterattack = true;
		public float outRange=15f;
			
		private BaseUnit owner;
		[SerializeField]
		private GameObject target;
		[SerializeField]
		private List<GameObject> detects =new List<GameObject> ();
		[SerializeField]
		private bool isIgnore;



		void Awake() {
			owner = GetComponent<BaseUnit> ();
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

		public void setTarget(GameObject enemyObject){
			if (!UnitUtils.CheckHostile (gameObject, enemyObject)) {
				return;
			}
			target = enemyObject;
		}

		IEnumerator coDetectEnemy() {
			while (true) {
				yield return new WaitForSeconds (0.1f);
				if (isIgnore || target != null ) {
					continue;
				}
				detects.Clear ();
				var units = BaseUnit.unitList.FindAll (x => UnitUtils.CheckHostile (owner, x) && !x.isInvincible && checkRange (x.gameObject));
				foreach (var it in units) {
					detects.Add (it.gameObject);
				}
				if (detects.Count > 0) {
					target=detects [0];

				}
			}
		}
		IEnumerator coAttackLoop() {
			while (true) {
				yield return new WaitForSeconds (1f/attackSpeed);
				if (isIgnore || target == null ) {
					continue;
				}
				if (checkRange (target)) {
					attack (target);
				} else {
					if (checkRange (target, outRange)) {
						chaseTarget ();
					} else {
						target = null;
					}
				}
			}
		}

		private void chaseTarget() {
			var movable=gameObject.GetComponent<Movable> ();
			if (movable==null) {
				return;
			}
			movable.toMove (target.transform.position,range*range*0.9f);
		}
		private bool checkRange(Vector3 targetPos,float dis) {
			var sqrLen = (targetPos - transform.position).sqrMagnitude;
			return sqrLen< dis*dis;
		}
		private bool checkRange(GameObject obj,float dis) {
			return checkRange (obj.transform.localPosition, dis);
		}
		private bool checkRange(GameObject obj) {
			return checkRange (obj.transform.localPosition, range);
		}

		//UnitEvent
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
	}
}
