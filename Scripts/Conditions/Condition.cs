using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;

namespace BSS {
    public abstract class Condition : SerializedMonoBehaviour
	{

		public abstract bool validate (object target);
	}
}

