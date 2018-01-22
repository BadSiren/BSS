using UnityEngine;
using System.Collections;

namespace BSS {
	public class SelectStateCondition : Condition
	{
		[Sirenix.OdinInspector.BoxGroup()]
		public string NeedParameterType="Void";
		public ESelectState eSelectState;

		public override bool validate (object target) {
            return BaseSelect.instance.eSelectState==eSelectState;
		}

	}
}

