using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using BSS.Unit;


namespace BSS.Event {
    public class SelectCondition : MonoBehaviour
	{
        [FoldoutGroup("State")]
		public ESelectState selectState;
        [FoldoutGroup("State")]
        public ESelectUnitState selectUnitState;

		public bool selectStateEqual () {
            return BaseSelect.instance.eSelectState==selectState;
		}
        public bool selectUnitStateEqual() {
            return BaseSelect.instance.eSelectUnitState == selectUnitState;
        }

	}
}

