using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS {
    public abstract class InItemAct : SerializedMonoBehaviour
    {
        public abstract void activate(BaseUnit owner);
    }
}
