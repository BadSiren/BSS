using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class UnitControl : MonoBehaviour
	{
		public BaseUnit targetUnit;

		public void setTargetUnit(GameObject obj) {
			targetUnit=obj.GetComponent<BaseUnit> ();
		}

		public void toMove(Vector2 pos) {
			if (targetUnit == null) {
				return;
			}
			var movable=targetUnit.GetComponent<Movable> ();
			if (movable == null) {
				return;
			}
			var charactable=targetUnit.GetComponent<Charactable> ();
			if (charactable == null) {
				movable.toMove (pos);
			} else {
				charactable.lookAtTarget (pos.x);
				charactable.playAnimMotion (Charactable.AnimType.Move, false);
				movable.toMove (pos,()=>{
					charactable.playAnimMotion (Charactable.AnimType.Idle, false);
				});
			}
		}
	}
}

