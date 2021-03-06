using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS.Event {
    public abstract class UnitGetter : SerializedMonoBehaviour
    {
        public System.Func<GameObject> targetFunc;

        protected GameObject target {
            get {
                return targetFunc();
            }
        }

        public GameObject getMainSelect() {
            if (BaseSelect.instance.mainSelectable == null) {
                return null;
            }
            return BaseSelect.instance.mainSelectable.gameObject;
        }
    }
}
