using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS.Event {
    public class UnitAttackableCondition : UnitCondition {
        public Attackable attackable {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponent<Attackable>();
            }
        }

        public bool existAttackable() {
            return attackable != null;
        }

    }
}
