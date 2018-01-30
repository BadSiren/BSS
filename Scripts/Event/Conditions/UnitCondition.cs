using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS.Event {
    public class UnitCondition : SerializedMonoBehaviour {
        public GameObject _target;
        public System.Func<GameObject> targetFunc;

        private GameObject target {
            get {
                if (targetFunc != null) {
                    return targetFunc();
                }
                return _target;
            }
        }

        public GameObject getSelectUnit() {
            if (BaseSelect.instance.mainSelectable == null) {
                return null;
            }
            return BaseSelect.instance.mainSelectable.owner.gameObject;
        }

        public bool equalMainSelect() {
            if (BaseSelect.instance.mainSelectable == null || target == null) {
                return false;
            }
            GameObject comparisonObj = BaseSelect.instance.mainSelectable.owner.gameObject;
            return target.GetInstanceID() == comparisonObj.GetInstanceID();
        }
        public bool equalMainSelect(GameObject obj) {
            if (BaseSelect.instance.mainSelectable == null) {
                return false;
            }
            GameObject comparisonObj = BaseSelect.instance.mainSelectable.owner.gameObject;
            return obj.GetInstanceID() == comparisonObj.GetInstanceID();
        }
        public bool isOnlyMine() {
            return target.GetComponent<BaseUnit>().onlyMine;
        }
        public bool isOnlyMine(GameObject obj) {
            return obj.GetComponent<BaseUnit>().onlyMine;
        }
    }
}
