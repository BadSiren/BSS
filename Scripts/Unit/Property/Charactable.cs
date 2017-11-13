using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	[RequireComponent (typeof (BaseUnit))]
	public class Charactable : MonoBehaviour
	{
		public Sprite portrait;
		private Character character;

		void Awake() {
			character=transform.Find("Character").GetComponent<Character> ();
		}
			
		public void lookAtTarget(float targetX) {
			character.lookAtTarget (targetX);
		}
		public void lookAtTarget(Vector3 target) {
			lookAtTarget (target.x);
		}

		public void idleMotion() {
			character.idleMotion ();
		}
		public void moveMotion(float speed=1f) {
			character.moveMotion (speed);
		}
		public void attackMotion(float speed=1f) {
			character.attackMotion (speed);
		}
		public void hitMotion(float speed=1f) {
			character.hitMotion (speed);
		}
		public void dieMotion(float speed=1f) {
			character.dieMotion (speed);
		}

		//UnitEvent
		private void onAllMoveEvent(Vector3 targetPos) {
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
	}
}

