using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class UnitControl : MonoBehaviour
	{
        public BaseUnit targetUnit;

        private Movable movable;
        private Attackable attackable;

		public void setTargetUnit(GameObject obj) {
			targetUnit=obj.GetComponent<BaseUnit> ();
            movable = targetUnit.GetComponent<Movable>();
            attackable = targetUnit.GetComponent<Attackable>();
		}

        public void toMove(Vector2 pos) {
            if (targetUnit == null || movable == null) {
                return;
            }
            if (attackable != null) {
                attackable.huntStop();
            }
            movable.followStop();
            movable.toMove(pos);
        }

        public void toFollow(GameObject enemyObj) {
            BaseUnit enemy = enemyObj.GetComponent<BaseUnit>();
            if (targetUnit == null || movable == null) {
                return;
            }
            if (attackable != null) {
                attackable.huntStop();
            }

            movable.toFollow(enemyObj,6f);
        }
        public void toAttack(GameObject enemyObj) {
            BaseUnit enemy = enemyObj.GetComponent<BaseUnit>();
            if (targetUnit == null || attackable == null) {
                return;
            }
            if (movable != null) {
                movable.toFollow(enemyObj, attackable.range*0.9f);
            }
            attackable.toHunt(enemyObj);
        }
    
		

        public void setSelectUnitState(string state) {
            BaseSelect.instance.eSelectUnitState = (ESelectUnitState)System.Enum.Parse(typeof(ESelectUnitState), state);
        }
	}
}

