using UnityEngine;
using System.Collections;
using BSS.Unit;


namespace BSS.Event {
    public class BaseUnitCondition : UnitCondition
    {
        public BaseUnit targetUnit {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponent<BaseUnit>();
            }
        }

        public bool isOnlyMine() {
            if (targetUnit == null) {
                return false;
            }
            return targetUnit.onlyMine;
        }
    }
}
