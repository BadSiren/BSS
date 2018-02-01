using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
    public class DistanceCondition : SerializedMonoBehaviour
	{
        public System.Func<GameObject> targetFunc;
        public System.Func<GameObject> comparisonFunc;
        public float distance;

		public bool vaildateRange() {
            if (targetFunc() == null || comparisonFunc()==null) {
                return false;
            }
            return Vector2.Distance (targetFunc().transform.position, comparisonFunc().transform.position) < distance;
		}

	}
}
