using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace BSS.Callback {
    public abstract class BSCallback : MonoBehaviour
    {
        public List<Component> listeners = new List<Component>();
    }
}
