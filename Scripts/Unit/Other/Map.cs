using UnityEngine;
using System.Collections;
using BSS.Unit;
using BSS.Input;

namespace BSS {
	public class Map : MonoBehaviour
	{
		public void selectedToMoveByForce() {
			if (Selectable.selectTeam != UnitTeam.Red) {
				return;
			}
			Vector3 mousePoint = BaseInput.getMousePoint2D ();
			foreach (var it in Selectable.selectedList) {
				it.SendMessage ("toMoveByForce", mousePoint, SendMessageOptions.DontRequireReceiver);
			}
		}
		public void selectedToMove() {
			if (Selectable.selectTeam != UnitTeam.Red) {
				return;
			}
			Vector3 mousePoint = BaseInput.getMousePoint2D ();
			foreach (var it in Selectable.selectedList) {
				it.SendMessage ("toMove", mousePoint, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}

