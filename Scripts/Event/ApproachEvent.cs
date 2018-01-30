using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS.Event {
    [RequireComponent(typeof(Collider2D))]
    public class ApproachEvent : BSEvent
    {
        [FoldoutGroup("Condition")]
        public List<System.Func<GameObject,bool>> trueTargetConditions = new List<System.Func<GameObject,bool>>();
        [FoldoutGroup("Condition")]
        public List<System.Func<GameObject,bool>> falseTargetConditions = new List<System.Func<GameObject,bool>>();



        public bool validate(GameObject obj) {
            foreach (var it in trueConditions) {
                if (it != null) {
                    if (!it.Invoke()) {
                        return false;
                    }
                }
            }
            foreach (var it in falseConditions) {
                if (it != null) {
                    if (it.Invoke()) {
                        return false;
                    }
                }
            }
            foreach (var it in trueTargetConditions) {
                if (it != null) {
                    if (!it.Invoke(obj)) {
                        return false;
                    }
                }
            }
            foreach (var it in falseTargetConditions) {
                if (it != null) {
                    if (!it.Invoke(obj)) {
                        return false;
                    }
                }
            }
            return true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var unit=other.GetComponentInParent<BaseUnit>();
            if (unit == null) {
                return;
            }
            if (validate(unit.gameObject)) {
                trueAction.Invoke();
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            var unit = other.GetComponentInParent<BaseUnit>();
            if (unit == null) {
                return;
            }
            if (validate(unit.gameObject)) {
                falseAction.Invoke();
            }
        }
    }
}
