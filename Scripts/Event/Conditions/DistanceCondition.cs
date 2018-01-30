using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
    public class DistanceCondition : SerializedMonoBehaviour
	{
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
        public GameObject _comparison;
        public System.Func<GameObject> comparisonFunc;
        private GameObject comparison {
            get {
                if (comparisonFunc != null) {
                    return comparisonFunc();
                }
                return _comparison;
            }
        }

        public float distance;

        public GameObject getSelectUnit() {
            if (BaseSelect.instance.mainSelectable == null) {
                return null;
            }
            return BaseSelect.instance.mainSelectable.owner.gameObject;
        }

		public bool vaildateRange() {
            if (target == null || comparison==null) {
                return false;
            }
            return Vector2.Distance (target.transform.position, comparison.transform.position) < distance;
		}

	}
}
