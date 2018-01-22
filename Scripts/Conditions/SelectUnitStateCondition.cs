using UnityEngine;
using System.Collections;

namespace BSS {
    public class SelectUnitStateCondition : Condition {
        [Sirenix.OdinInspector.BoxGroup()]
        public string NeedParameterType = "Void";
        public ESelectUnitState eSelectUnitState;

        public override bool validate(object target) {
            return BaseSelect.instance.eSelectUnitState == eSelectUnitState;
        }

    }
}

