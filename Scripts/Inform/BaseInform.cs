using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
    public class BaseInform : SerializedMonoBehaviour
    {
        public static BaseInform instance;

        void Awake() {
            instance = this;
        }
    }
}
