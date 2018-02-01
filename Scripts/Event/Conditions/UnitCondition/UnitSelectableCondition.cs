using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS.Event {
    public class UnitSelectableCondition : UnitCondition
    {
        public bool isMainSelect() {
            if (target == null && BaseSelect.instance.mainSelectable == null ) {
                return false;
            }
            GameObject selectObj = BaseSelect.instance.mainSelectable.owner.gameObject;
            return target.GetInstanceID() == selectObj.GetInstanceID();
        }

    }
}
