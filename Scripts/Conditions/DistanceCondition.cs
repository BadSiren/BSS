using UnityEngine;
using System.Collections;

namespace BSS {
	public class DistanceCondition : Condition
	{
		[Sirenix.OdinInspector.BoxGroup()]
		public string NeedParameterType="GameObject";
		public float distance;
        public System.Func<GameObject> comparisonFunc;

        private GameObject comparisonObj;
        private GameObject targetObj;

		public override bool validate (object target) {
            if (comparisonFunc == null || comparisonFunc()==null) {
                return false;
            }
            comparisonObj = comparisonFunc();
	        targetObj = (target as GameObject);
			return Vector2.Distance (targetObj.transform.position, comparisonObj.transform.position) < distance;
		}

	}
}
