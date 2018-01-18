using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
	public class SelectStateCondition : Condition
	{
		[Sirenix.OdinInspector.BoxGroup()]
		public string NeedParameterType="Void";
		public ESelectState selectState;

		public override bool validate (object target) {
			return BaseSelect.instance.eSelectState==selectState;
		}

	}
}

