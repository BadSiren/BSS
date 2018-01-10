using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	[RequireComponent (typeof (BaseUnit))]
	public class Charactable : SerializedMonoBehaviour
	{
		public enum AnimType
		{
			Idle,Move,Attack,Hit,Die
		}

		public bool isLookRight;
		public Dictionary<AnimType,string> animationDics=new Dictionary<AnimType,string>();

		private PhotonView photonView;
		private BaseUnit owner;
		private Animator anim;


		void Awake() {
			owner = GetComponent<BaseUnit> ();
			photonView = GetComponent<PhotonView>();
			anim=transform.Find("Character").GetComponent<Animator> ();
		}
		void Start() {
			playAnimMotion (AnimType.Idle,false);
		}
			
		public void lookAtTarget(float targetX) {
			if (targetX > transform.position.x && !isLookRight) {
				setMirror ();
			}
			if (targetX < transform.position.x && isLookRight) {
				setMirror ();
			}
		}

		public void playAnimMotion(AnimType animType,bool reset,float speed=1f) {
			if (!owner.isMine) { 
				return;
			}
			photonView.RPC ("recvPlayAnimMotion", PhotonTargets.All,animType.ToString(),reset,speed);
		}
		[PunRPC]
		void recvPlayAnimMotion(string animTypeString,bool reset,float speed,PhotonMessageInfo mi) {
			AnimType animType = (AnimType)System.Enum.Parse (typeof(AnimType), animTypeString);
			setAnimationSpeed(speed);
			playAnimation(animationDics [animType],reset);
		}

		private void playAnimation(string _anim, bool _reset)
		{
			if (anim == null) {
				return;
			}
			if (_reset) {	
				anim.Play(_anim, -1, 0f);
			} else {
				anim.CrossFade(_anim, 0.1f);
			}
		}
		private void setAnimationSpeed(float _value)
		{
			if (anim == null) {
				return;
			}
			anim.speed = _value;
		}

		private void setMirror(){
			transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			isLookRight = !isLookRight;
		}

		//UnitEvent
		/*
		private void onAllMoveEvent(Vector2 targetPos) {
			lookAtTarget (targetPos);
			moveMotion ();
		}
		private void onMoveStopEvent() {
			idleMotion ();
		}
		private void onAttackEvent(AttackInfo attackInfo) {
			lookAtTarget (attackInfo.hitter.transform.position);
			attackMotion (attackInfo.attackSpeed);
		}
		private void onDieEvent() {
			dieMotion ();
		}
		*/
	}
}

