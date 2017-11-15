using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS {
	[RequireComponent(typeof(Collider2D))]
	public class Chasable : MonoBehaviour {
		public Chasable(Transform _target,float _speed) {
			target = _target;
		}

		public bool isOnlyTarget;
		public GameObject owner;
		public Transform target;
		public float speed;


		void Update() {
			if (target != null) {
				owner.transform.position = Vector3.MoveTowards (owner.transform.position, target.localPosition, speed * Time.deltaTime);
			}
		}
		public void OnTriggerEnter2D(Collider2D col) {
			if (isOnlyTarget) {
				if (col.gameObject.GetInstanceID () == target.gameObject.GetInstanceID ()) {
					Destroy (this);
				}
			} else {
				if (col.tag == "Enemy") {
					Destroy (this);
				}
			}
		}

	}
}