using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
	public class SelectStateCondition : Condition
	{
		[BoxGroup("NeedParameter")]
		public string ParameterType="Void";
		public ESelectState selectState;

		public override bool validate (object target) {
			return BaseSelect.instance.eSelectState==selectState;
		}

	}
}

