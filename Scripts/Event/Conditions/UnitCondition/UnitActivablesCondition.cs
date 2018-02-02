using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class UnitActivablesCondition : UnitCondition
    {
        public Activables activables {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponentInChildren<Activables>();
            }
        }
        public bool isSelected(int index) {
            if (activables == null) {
                return false;
            }
            if (activables.selectedAct == -1) {
                return false;
            }
            return activables.selectedAct == index;
        }
        public bool existSelected() {
            if (activables == null) {
                return false;
            }
            return activables.selectedAct != -1;
        }

    }
}
