using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    public class UnitBody : MonoBehaviour,IMoveReact
	{
		public float mass = 1f;
		private Rigidbody2D rigid;

		void Awake() {
			rigid = gameObject.AddComponent<Rigidbody2D> ();
			rigid.gravityScale = 0f;
			rigid.mass = mass;
			rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

        //Interface
        public void onMove(Vector2 pos, float speed) {
            rigid.mass = mass + 10f;
        }
        public void onStop() {
            rigid.mass = mass ;
        }
	}
}

