using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class MoveBeacon : MonoBehaviour
	{
		public Transform destinaionTarget;
		public Vector3 destination;
		public UnitTeam applyTeam;

		void Awake() {
			if (destinaionTarget != null) {
				destination = destinaionTarget.localPosition;
			}
		}

		void OnTriggerEnter2D(Collider2D col) {
			if (col.tag == "Ignore") {
				return;
			}
			BaseUnit unit=col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || !validate(unit)) {
				return;
			}
			
			unit.gameObject.SendMessage ("toMove", destination, SendMessageOptions.DontRequireReceiver);
		}
			
		private bool validate(BaseUnit unit) {
			if (unit.team != applyTeam) {
				return false;
			}
			return true;
		}
	}
}

