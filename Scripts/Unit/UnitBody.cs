using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	[RequireComponent (typeof (BaseUnit))]
	public class UnitBody : MonoBehaviour
	{
		public float mass = 1f;
		private Rigidbody2D rigid;

		void Awake() {
			rigid = gameObject.AddComponent<Rigidbody2D> ();
			rigid.gravityScale = 0f;
			rigid.mass = mass;
			rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		private void onAllMoveEvent() {
			rigid.mass = mass+10f;
		}
		private void onMoveStopEvent() {
			rigid.mass = mass;
		}
	}
}

