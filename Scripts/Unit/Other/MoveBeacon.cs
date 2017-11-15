using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class MoveBeacon : MonoBehaviour
	{
		public Transform destinaionTarget;
		public Vector3 destination;
		public UnitTeam applyTeam;
		public bool isIgnore;
		public bool isForce;

		void Awake() {
			if (destinaionTarget != null) {
				destination = destinaionTarget.localPosition;
			}
		}

		void OnTriggerEnter2D(Collider2D col) {
			if (col.tag == "Ignore" || isIgnore) {
				return;
			}
			BaseUnit unit=col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || !validate(unit)) {
				return;
			}
			if (isForce) {
				unit.gameObject.SendMessage ("toMoveByForce", destination, SendMessageOptions.DontRequireReceiver);
			} else {
				unit.gameObject.SendMessage ("toMove", destination, SendMessageOptions.DontRequireReceiver);
			}

		}
			
		private bool validate(BaseUnit unit) {
			if (unit.team != applyTeam) {
				return false;
			}
			return true;
		}
	}
}

