using UnityEngine;
using System.Collections;
using BSS.Unit;
using BSS.Input;

namespace BSS {
	public class Map : MonoBehaviour
	{
		public void selectedToMoveByForce(Vector3 targetPos) {
			if (Selectable.selectTeam != UnitTeam.Red) {
				return;
			}
			foreach (var it in Selectable.selectedList) {
				it.SendMessage ("toMoveByForce", targetPos, SendMessageOptions.DontRequireReceiver);
			}
		}
		public void selectedToMove(Vector3 targetPos) {
			if (Selectable.selectTeam != UnitTeam.Red) {
				return;
			}
			foreach (var it in Selectable.selectedList) {
				it.SendMessage ("toMove", targetPos, SendMessageOptions.DontRequireReceiver);
			}
		}

		private void onDoubleClickEvent() {
			Vector3 mousePoint = BaseInput.getMousePoint2D ();
			selectedToMoveByForce (mousePoint);
		}
		private void onLongClickEvent() {
			Vector3 mousePoint = BaseInput.getMousePoint2D ();
			selectedToMove (mousePoint);
		}
	}
}

