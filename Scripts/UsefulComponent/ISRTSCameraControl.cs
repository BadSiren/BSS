using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
    public class ISRTSCameraControl: SerializedMonoBehaviour
    {
        public GameObject target;
        public System.Func<GameObject> targetFunc;

        public void follow() {
            if (target != null) {
                ISRTSCamera2D.JumpToTargetForMain(target.transform);
            }
            if (targetFunc() != null) {
                var obj = targetFunc();
                ISRTSCamera2D.JumpToTargetForMain(obj.transform);
            }
        }
    }
}
