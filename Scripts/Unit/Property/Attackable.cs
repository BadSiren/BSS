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
		private float _changeRange;
		public float changeRange {
			get {
				return _changeRange;
			}
			set {
				_changeRange=value;
				attackCollider.setRadius (range);
			}
		}
		public float range {
			get {
				return initRange+changeRange;
			}
		}
			
		private BaseUnit owner;
		[SerializeField]
		private GameObject target;
		[SerializeField]
		public List<GameObject> targets =new List<GameObject> ();
		[SerializeField]
		public List<GameObject> reserveTargets =new List<GameObject> ();
		private bool isIgnore;

		private AttackCollider attackCollider;
		private MessageArgsTwo args;


		void Awake() {
			owner = GetComponent<BaseUnit> ();
			attackableList.Add(this);

			initAttackCollider ();

		}
		void Start (){
			changeRange = 0f;
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


		IEnumerator attackLoop() {
			while (true) {
				yield return new WaitForSeconds (1f/attackSpeed);
				if (target != null) {
					attack (target);
				}

			}
		}

		private void initAttackCollider() {
			GameObject obj = new GameObject ("AttackRange");
			obj.transform.SetParent (gameObject.transform,false);
			attackCollider=obj.AddComponent <AttackCollider>();
			attackCollider.attakable = this;
		}


		public void OnTriggerEnterTarget(Collider2D col) {
			if (col.tag == "Ignore"||col is CircleCollider2D) {
				return;
			}
			BaseUnit unit=col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || !checkHostile(unit) || unit.isInvincible)
			{
				return;
			}

			if (reserveTargets.Contains (col.gameObject)) {
				reserveTargets.Remove (col.gameObject);
				SendMessage ("onExitReserveEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				if (reserveTargets.Count == 0) {
					SendMessage ("onNothingReserveEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}
			if (!targets.Contains (col.gameObject)) {
				targets.Add (col.gameObject);
				SendMessage ("onEnterTargetEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				if (targets.Count == 1) {
					target = targets [0];
					SendMessage ("onFirstTargetEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		public void OnTriggerExitTarget(Collider2D col) {
			if (col.tag == "Ignore" ||  col is CircleCollider2D) {
				return;
			}
			BaseUnit unit=col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || !checkHostile(unit) || unit.isInvincible)
			{
				return;
			}
			if (!reserveTargets.Contains (col.gameObject) && col.tag!="Die") {
				reserveTargets.Add (col.gameObject);
				SendMessage ("onEnterReserveEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				if (reserveTargets.Count == 1) {
					SendMessage ("onFirstReserveEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}
			if (targets.Contains (col.gameObject)) {
				targets.Remove (col.gameObject);
				if (target != null && target.GetInstanceID () == col.gameObject.GetInstanceID ()) {
					findEnemy (ref target); 
				}
				SendMessage ("onExitTargetEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				if (targets.Count == 0) {
					SendMessage ("onNothingTargetEvent",  SendMessageOptions.DontRequireReceiver);
				}
			}

		}


		private bool findEnemy(ref GameObject obj) {
			if (targets.Count == 0) {
				obj = null;
				return false;
			} 
			obj=targets [0];
			return true;
		}

		private bool checkRange(Vector3 targetPos,float dis) {
			return Vector3.Distance (transform.localPosition, targetPos) < dis;
		}
		private bool checkRange(GameObject obj,float dis) {
			return Vector3.Distance (transform.localPosition, obj.transform.localPosition) < dis;
		}

		private bool checkHostile(UnitTeam team,UnitTeam other) {
			if ((team == UnitTeam.White || other == UnitTeam.White) || team == other) {
				return false;
			} 
			return true;
		}
		private bool checkHostile(BaseUnit enemyUnit) {
			return checkHostile (gameObject.GetComponent<BaseUnit> ().team, enemyUnit.team);
		}

		//UnitEvent
		private void onInitialize() {
			if (initRange > RANGETYPE) {
				attackType = AttackType.Long;
			} else {
				attackType = AttackType.Short;
			}
			StartCoroutine(attackLoop());
		}

		private void onEnterDetectedEvent(GameObject obj) {
			if (!reserveTargets.Contains (obj)) {
				reserveTargets.Add (obj);
				if (reserveTargets.Count == 1) {
					SendMessage ("onFirstReserveEvent", obj, SendMessageOptions.DontRequireReceiver);
				}
				SendMessage ("onEnterReserveEvent", obj, SendMessageOptions.DontRequireReceiver);
			}
		}
		private void onExitDetectedEvent(GameObject obj) {
			if (reserveTargets.Contains (obj)) {
				reserveTargets.Remove (obj);
				SendMessage ("onExitReserveEvent", obj, SendMessageOptions.DontRequireReceiver);
				if (reserveTargets.Count == 0) {
					SendMessage ("onNothingReserveEvent", obj, SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		/*
		private void onToMoveEvent(Vector3 targetPos) {
			isIgnore = true;
			if (target != null) {
				target = null;
				SendMessage ("onExitTargetEvent", SendMessageOptions.DontRequireReceiver);
			}
		}
		private void onToPatrolEvent(Vector3 targetPos) {
			isIgnore = false;
			if (target==null && findEnemy (ref target)) {
				SendMessage ("onFindTargetEvent", SendMessageOptions.DontRequireReceiver);
			}
		}
		private void onMoveStopEvent() {
			isIgnore = false;
			if (target==null && findEnemy (ref target)) {
				SendMessage ("onFindTargetEvent", SendMessageOptions.DontRequireReceiver);
			}
		}

		private void onHitEvent(AttackInfo attackInfo) {
			if (isIgnore || target!=null) {
				return;
			}
			SendMessage ("toPatrolStopover",attackInfo.attacker.transform.localPosition, SendMessageOptions.DontRequireReceiver);
		}
		*/





	}
}
