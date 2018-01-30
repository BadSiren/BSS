using UnityEngine;
using System.Collections;
using BSS.Unit;
using Sirenix.OdinInspector;

namespace BSS.Event {
    public class UnitControl : SerializedMonoBehaviour
    {
        [FoldoutGroup("TargetUnit")]
        public BaseUnit targetUnit;
        [FoldoutGroup("TargetUnit")]
        public System.Func<BaseUnit> unitFunc;
        [FoldoutGroup("TargetUnit")]
        public System.Func<GameObject> unitObjFunc;
        [FoldoutGroup("ToMoveEvent")]
        public Vector2 vector;
        [FoldoutGroup("ToMoveEvent")]
        public System.Func<Vector2> vectorFunc;

        public virtual BaseUnit unit {
            get {
                if (unitFunc != null) {
                    return unitFunc();
                }
                if (unitObjFunc != null) {
                    return unitObjFunc().GetComponent<BaseUnit>();
                }
                return targetUnit;
            }
        }

        public Attackable attackable {
            get {
                if (unit== null) {
                    return null;
                }
                return unit.GetComponent<Attackable>();
            }
        }
        public Movable movable {
            get {
                if (unit == null) {
                    return null;
                }
                return unit.GetComponent<Movable>();
            }
        }
        public Itemable itemable {
            get {
                if (unit == null) {
                    return null;
                }
                return unit.GetComponent<Itemable>();
            }
        }

        public void toMove(Vector2 pos) {
            if (unit == null || movable == null) {
                return;
            }
            if (attackable != null) {
                attackable.huntStop();
            }
            movable.followStop();
            movable.toMove(pos);
        }
        public void toMove() {
            Vector2 vec = vector;
            if (vectorFunc != null) {
                vec = vectorFunc();
            }
            toMove(vec);
        }

        public void toFollow(GameObject enemyObj) {
            if (unit == null || movable == null) {
                return;
            }
            if (attackable != null) {
                attackable.huntStop();
            }

            movable.toFollow(enemyObj, 6f);
        }
        public void toAttack(GameObject enemyObj) {
            if (unit == null || attackable == null) {
                return;
            }
            if (movable != null) {
                movable.toFollow(enemyObj, attackable.range * 0.9f);
            }
            attackable.toHunt(enemyObj);
        }
        public void toThrow() {
            if (unit == null || itemable == null) {
                return;
            }
            if (itemable.selectedItem == -1) {
                return;
            }
            itemable.throwItem(itemable.selectedItem);
        }



        public void setSelectUnitState(string state) {
            BaseSelect.instance.eSelectUnitState = (ESelectUnitState)System.Enum.Parse(typeof(ESelectUnitState), state);
        }
    }
}
