using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class UnitActivablesCondition : UnitCondition
    {
        public Activables targetActivables {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponentInChildren<Activables>();
            }
        }
        public bool isSelected(int index) {
            if (targetActivables == null) {
                return false;
            }
            if (targetActivables.selectedAct == -1) {
                return false;
            }
            return targetActivables.selectedAct == index;
        }


    }
}
