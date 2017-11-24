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
		private const float SIGHT=20f;

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
			
		private BaseUnit owner;
		[SerializeField]
		private GameObject target;
		[SerializeField]
		private List<GameObject> targets =new List<GameObject> ();
		[SerializeField]
		private List<GameObject> detects =new List<GameObject> ();


		public AttackState state;
		public enum AttackState {
			Idle,Detect,Attack,Ignore
		}

		private AttackCollider detectCollider;
		private AttackCollider attackCollider;


		void Awake() {
			owner = GetComponent<BaseUnit> ();
			attackableList.Add(this);

			initDetectCollider ();
			initAttackCollider ();
		}
		void Start (){
			setIdle ();
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

		public void setIgnore() {
			state = AttackState.Ignore;
			detectCollider.setDisable ();
			attackCollider.setDisable ();
			target = null;

		}
		public void setIdle() {
			state = AttackState.Idle;
			detectCollider.setEnable (SIGHT);
		}

		IEnumerator attackLoop() {
			while (true) {
				yield return new WaitForSeconds (1f/attackSpeed);
				if (state == AttackState.Ignore) {
					continue;
				}

				if (target == null) {
					if (state != AttackState.Idle) {
						state = AttackState.Idle;
						detectCollider.setEnable (SIGHT);
					}
				} else {
					attack (target);
				}
			}
		}


		private void initAttackCollider() {
			GameObject obj = new GameObject ("AttackRange");
			obj.transform.SetParent (gameObject.transform,false);
			attackCollider=obj.AddComponent <AttackCollider>();
			attackCollider.attakable = this;
			attackCollider.triggerEnter = "OnTriggerEnterTarget";
			attackCollider.triggerExit = "OnTriggerExitTarget";
			attackCollider.setDisable ();
		}
		private void initDetectCollider() {
			GameObject obj = new GameObject ("DetectRange");
			obj.transform.SetParent (gameObject.transform,false);
			detectCollider=obj.AddComponent <AttackCollider>();
			detectCollider.attakable = this;
			detectCollider.triggerEnter = "OnTriggerEnterDetect";
			detectCollider.triggerExit = "OnTriggerExitDetect";
			detectCollider.setDisable ();
		}
		//Attack Collider Trigger
		private void OnTriggerEnterDetect(Collider2D col) {
			if (state == AttackState.Ignore) {return;}

			if (state==AttackState.Idle && checkHostile (col.gameObject) ) {
				detectCollider.setDisable ();
				state = AttackState.Detect;
				SendMessage("toMove",col.gameObject.transform.localPosition,SendMessageOptions.DontRequireReceiver);
				attackCollider.setEnable (range);
			}
		}
		private void OnTriggerEnterTarget(Collider2D col) {
			if (state == AttackState.Ignore) {return;}
			if (target != null) {return;}

			if (checkHostile (col.gameObject) && state==AttackState.Detect) {
				state = AttackState.Attack;
				target = col.gameObject;
				SendMessage("moveStop",SendMessageOptions.DontRequireReceiver);
			}
		}
		private void OnTriggerExitTarget(Collider2D col) {
			if (state == AttackState.Ignore) {return;}
			if (target == null) {return;}

			if (target.gameObject.GetInstanceID () == col.gameObject.GetInstanceID ()) {
				target = null;
				state = AttackState.Idle;
				attackCollider.setDisable ();
				detectCollider.setEnable (SIGHT);
			}
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
			return checkHostile (team, enemyUnit.team);
		}
		private bool checkHostile(GameObject enemy) {
			BaseUnit unit = enemy.GetComponent<BaseUnit> ();
			return checkHostile (team, unit.team);
		}

		//UnitEvent
		private void onInitialize() {
			StartCoroutine(attackLoop());
		}
		private void onMoveByForceEvent() {
			setIgnore ();
		}
		private void onMoveStopEvent() {
			if (state == AttackState.Ignore) {
				setIdle ();
			}
		}

	}
}
