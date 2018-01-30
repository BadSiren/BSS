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
        [HideInInspector]
        public BaseUnit owner;
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

        [FoldoutGroup("ItemProperty")]
        public string speedProperty = "MoveSpeed";

        private PolyNavAgent navAgent;
        private GameObject followTarget;
        private float followDistance;
	

		void Awake() {
			owner = GetComponent<BaseUnit> ();
			navAgent=GetComponent<PolyNavAgent> ();
			movableList.Add(this);
			navAgent.maxSpeed = speed;
            StartCoroutine(coFollowTarget());
		}
		void OnDestroy()
		{
			movableList.Remove(this);
		}



		public void toMove(Vector2 targetPos) {
            var moveReacts=GetComponents<IMoveReact>();
            foreach (var it in moveReacts) {
                it.onMove(targetPos,speed);
            }
            navAgent.SetDestination(targetPos, (x) => {
                moveStop();
            });
		}
		public void toMove(Vector2 targetPos,System.Action stopAct) {
            var moveReacts = GetComponents<IMoveReact>();
            foreach (var it in moveReacts) {
                it.onMove(targetPos,speed);
            }
            navAgent.SetDestination (targetPos,(x)=> {
                if (stopAct != null) {
                    stopAct.Invoke();
                }
                moveStop();
			});
		}
        public void moveStop() {
            navAgent.Stop();
            var moveReacts = GetComponents<IMoveReact>();
            foreach (var it in moveReacts) {
                it.onStop();
            }
        }

		public void toFollow(GameObject target,float distance) {
            followTarget = target;
            followDistance = distance;
		}
        public void followStop() {
            followTarget = null;
        }

        IEnumerator coFollowTarget() {
            while (true) {
                yield return new WaitForSeconds(0.25f);
                if (followTarget == null) {
                    continue;
                }
                if (!UnitUtils.InDistance(gameObject, followTarget, followDistance)) {
                    toMove(followTarget.transform.position);
                } else {
                    moveStop();
                }
            }
        }

        //Interface
        public void applyProperty(string ID,float value) {
            if (ID != speedProperty) {
                return;
            }
            changeSpeed += value;
        }
        public void cancleProperty(string ID,float value) {
            if (ID != speedProperty) {
                return;
            }
            changeSpeed -= value;
        }
	}
}

