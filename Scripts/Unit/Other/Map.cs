using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS {
	public class Map : MonoBehaviour
	{
		public void toMoveAllEvent(Vector3 targetPos) {
			foreach (var it in Movable.movableList) {
				if (it.canInputing) {
					it.toMove (targetPos);

				}
			}
		}
		public void toPatrolAllEvent(Vector3 targetPos) {
			foreach (var it in Movable.movableList) {
				if (it.canInputing) {
					it.toPatrol (targetPos);
				}
			}
		}
	}
}

