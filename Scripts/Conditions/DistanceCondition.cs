using UnityEngine;
using System.Collections;

namespace BSS {
	public class DistanceCondition : Condition
	{
		[Sirenix.OdinInspector.BoxGroup("NeedParameter")]
		public string ParameterType="GameObject";
		public GameObject comparisonObj;
		public float distance;

		public override bool validate (object target) {
			if (comparisonObj == null) {
				return false;
			}
			GameObject targetObj = (target as GameObject);
			return Vector2.Distance (targetObj.transform.position, comparisonObj.transform.position) < distance;
		}

		public void setComparisonObj(GameObject obj) {
			comparisonObj = obj;
		}

	}
}
