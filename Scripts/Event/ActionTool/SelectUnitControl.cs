using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class SelectUnitControl : UnitControl
	{
        public override BaseUnit unit {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.owner;
            }
        }
	}
}

