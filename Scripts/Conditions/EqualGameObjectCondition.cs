using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
	public class EqualGameObjectCondition : Condition
	{
		[BoxGroup("NeedParameter")]
		public string ParameterType="GameObject";
		public GameObject comparisonObj;

		public override bool validate (object target) {
			if (comparisonObj == null) {
				return false;
			}
			GameObject targetObj = (target as GameObject);
			return targetObj.GetInstanceID () == comparisonObj.GetInstanceID ();
		}

		public void setComparisonObj(GameObject obj) {
			comparisonObj = obj;
		}

	}
}
