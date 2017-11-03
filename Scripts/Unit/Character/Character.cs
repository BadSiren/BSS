using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	[RequireComponent(typeof(Animator))]
	public class Character : MonoBehaviour
	{
		public bool isLookRight;
		private Animator anim;
		public float idleAnimationSpeed = 1f;
		public string idleAnimaionName="idle";
		public string moveAnimaionName="walk";
		public string attackAnimaionName="attack";
		public string hitAnimaionName="hit";
		public string dieAnimaionName="death";

		void Awake() {
			anim=GetComponent<Animator> ();
		}
		void Start() {
			idleMotion ();
		}


		//Character Image Event
		public void lookAtTarget(float targetX) {
			if (targetX > transform.position.x && !isLookRight) {
				setMirror ();
			}
			if (targetX < transform.position.x && isLookRight) {
				setMirror ();
			}
		}
		public void PlayAnimation(string _anim, bool _reset)
		{
			if (_reset) {	
				anim.Play(_anim, -1, 0f);
			} else {
				anim.CrossFade(_anim, 0.1f);
			}
		}
		public void SetAnimationSpeed(float _value)
		{
			anim.speed = _value;
		}
		public void idleMotion() {
			SetAnimationSpeed(idleAnimationSpeed);
			PlayAnimation(idleAnimaionName,false);
		}
		public void moveMotion(float speed=1f) {
			SetAnimationSpeed(speed);
			PlayAnimation(moveAnimaionName,false);
		}
		public void attackMotion(float speed=1f) {
			SetAnimationSpeed(speed);
			PlayAnimation(attackAnimaionName,true);
		}
		public void hitMotion(float speed=1f) {
			SetAnimationSpeed(speed);
			PlayAnimation(hitAnimaionName,true);
		}
		public void dieMotion(float speed=1f) {
			SetAnimationSpeed(speed);
			PlayAnimation(dieAnimaionName,false);
		}

		private void setMirror(){
			transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			isLookRight = !isLookRight;
		}
	}
}

