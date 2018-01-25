using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class SelectUnitControl : MonoBehaviour
	{
        public BaseUnit selectUnit {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.owner;
            }
        }
        public Attackable attackable {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.owner.GetComponent<Attackable>();
            }
        }
        public Movable movable {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.owner.GetComponent<Movable>();
            }
        }

        public void toMove(Vector2 pos) {
            if (selectUnit == null || movable == null) {
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
            if (selectUnit == null || movable == null) {
                return;
            }
            if (attackable != null) {
                attackable.huntStop();
            }

            movable.toFollow(enemyObj,6f);
        }
        public void toAttack(GameObject enemyObj) {
            BaseUnit enemy = enemyObj.GetComponent<BaseUnit>();
            if (selectUnit == null || attackable == null) {
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

