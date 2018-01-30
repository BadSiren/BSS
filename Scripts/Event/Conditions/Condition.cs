using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;

namespace BSS {
    public abstract class Condition : SerializedMonoBehaviour
	{
        public bool validate(GameObject obj) { return true; }
	}
}

