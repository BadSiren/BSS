using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    public class ActSelectUnitStateChange : Activable {
        public ESelectUnitState eSelectUnitState;

        public override void initialize() {
            
        }
        public override void activate() {
            BaseSelect.instance.eSelectUnitState = eSelectUnitState;
        }

    }
}
