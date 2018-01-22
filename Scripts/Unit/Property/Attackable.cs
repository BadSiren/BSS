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
	[RequireComponent (typeof (BaseUnit))]
	public class Attackable : SerializedMonoBehaviour
	{
		public static List<Attackable> attackableList = new List<Attackable>();

		public Sprite icon;
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

        private GameObject huntTarget;
        private GameObject target;
		

        private bool canAttack = true;
		private BaseUnit owner;

        private Movable _movable;
        private Movable movable {
            get {
                return _movable == null ? GetComponent<Movable>() : _movable;
            }
        }
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
            attackableList.Add(this);
            StartCoroutine(coHuntTarget());
		}

		void OnDestroy()
		{
			attackableList.Remove(this);
		}

 
        public void toAttack(GameObject enemyObj) {
            if (!canAttack || isMoving) {
                return;
            }
            canAttack = false;
            Invoke("enableAttack", 1f / attackSpeed);

            var reacts = GetComponents<IAttackReact>();
            foreach (var it in reacts) {
                it.onAttack((enemyObj));
            }
            enemyObj.GetComponent<BaseUnit>().hitDamage(damage);
		}

        public void toHunt(GameObject enemyObj) {
            huntTarget = enemyObj;
        }
        public void huntStop() {
            huntTarget = null;
        }
        IEnumerator coHuntTarget() {
            while (true) {
                yield return new WaitForSeconds(0.1f);
                if (huntTarget == null) {
                    continue;
                }
                if (UnitUtils.InDistance(gameObject, huntTarget, range)) {
                    toAttack(huntTarget);
                } 
            }
        }

        private void enableAttack() {
            canAttack = true;
        }



        //TODO change

        /*
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
			
        private bool targetInRange() {
            if (target!=null) {
                Vector2 targetLocation = target.transform.position;
                Vector2 direction = targetLocation - (Vector2)transform.position;
                if (direction.sqrMagnitude < range * range) {
                    return true;
                }
            }
            return false;
        }
		private bool checkRange(BaseUnit unit,float dis) {
			return UnitUtils.IsUnitInCircle (unit, transform.position, dis);
		}
		private bool checkRange(BaseUnit unit) {
			return UnitUtils.IsUnitInCircle (unit, transform.position, range);
		}
        */


	}
}
