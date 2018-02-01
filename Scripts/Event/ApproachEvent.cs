using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS.Event {
    [RequireComponent(typeof(Collider2D))]
    public class ApproachEvent : BSEvent
    {
        public GameObject approachObj {
            get; set;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var unit=other.GetComponentInParent<BaseUnit>();
            if (unit == null) {
                return;
            }
            approachObj = unit.gameObject;
            if (validate()) {
                trueAction.Invoke();
            } else {
                falseAction.Invoke();
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            var unit = other.GetComponentInParent<BaseUnit>();
            if (unit == null) {
                return;
            }
            if (approachObj != null && approachObj.GetInstanceID() == unit.gameObject.GetInstanceID()) {
                approachObj = null;
            }
        }
    }
}
