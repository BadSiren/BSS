using UnityEngine;
using System.Collections;

namespace BSS {
	public class EqualMainSelectUnitCondition : Condition
	{
		[Sirenix.OdinInspector.BoxGroup()]
		public string NeedParameterType="GameObject";
		public bool validate (object target) {
			if (BaseSelect.instance.mainSelectable == null) {
				return false;
			}
			GameObject comparisonObj = BaseSelect.instance.mainSelectable.owner.gameObject;
			GameObject targetObj= (target as GameObject);

			return targetObj.GetInstanceID () == comparisonObj.GetInstanceID ();
		}
	}
}

