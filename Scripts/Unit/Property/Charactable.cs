using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[RequireComponent (typeof (BaseUnit))]
    public class Charactable : SerializedMonoBehaviour,IMoveReact,IAttackReact,IHitReact
	{
		public bool isLookRight;
        public string moveFloat = "speed";
        public string attackTrigger = "attack";
        public string hitTrigger = "hit";

		private BaseUnit owner;
		private Animator anim;


		void Awake() {
			owner = GetComponent<BaseUnit> ();
			anim=GetComponentInChildren<Animator> ();

		}
		void Start() {
            anim.SetFloat(moveFloat,0f);
		}
			
		public void lookAtTarget(float targetX) {
			if (targetX > transform.position.x && !isLookRight) {
				setMirror ();
			}
			if (targetX < transform.position.x && isLookRight) {
				setMirror ();
			}
		}

        public void setFloat(string _name,float _value) {
            if (!owner.photonView.isMine) {
                return;
            }
            owner.photonView.RPC("recvSetFloat", PhotonTargets.All, _name, _value);
        }
        [PunRPC]
        void recvSetFloat(string _name, float _value) {
            anim.SetFloat(_name, _value);
        }
        public void setTrigger(string _name) {
            if (!owner.photonView.isMine) {
                return;
            }
            owner.photonView.RPC("recvSetTrigger", PhotonTargets.All, _name);
        }
        [PunRPC]
        void recvSetTrigger(string _name) {
            anim.SetTrigger(_name);
        }


		private void setMirror(){
			transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			isLookRight = !isLookRight;
		}



        //Interface
        public void onMove(Vector2 pos,float speed) {
            lookAtTarget(pos.x);
            setFloat(moveFloat, speed);
        }
        public void onStop() {
            setFloat(moveFloat, 0f);
        }
        public void onAttack(GameObject obj) {
            lookAtTarget(obj.transform.position.x);
            setTrigger(attackTrigger);
        }
        public void onHit() {
            setTrigger(hitTrigger);
        }
	}
}

