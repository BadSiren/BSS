using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS {
    public class RelationCondition : SerializedMonoBehaviour
    {
        public System.Func<GameObject> targetFunc;
        public System.Func<GameObject> comparisonFunc;


        public bool isDifferentPlayer() {
            if (targetFunc() == null || comparisonFunc() == null) {
                return false;
            }
            var targetUnit=targetFunc().GetComponent<BaseUnit>();
            var comparisonUnit = comparisonFunc().GetComponent<BaseUnit>();
            if (targetUnit == null || comparisonUnit == null) {
                return false;
            }
            return targetUnit.photonView.ownerId != comparisonUnit.photonView.ownerId;
        }
    }
}
