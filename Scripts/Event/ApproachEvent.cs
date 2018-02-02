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
        [TabGroup("GameObject")]
        public GameObjectEvent gameObjectTrueAction;
        [TabGroup("GameObject")]
        public GameObjectEvent gameObjectFalseAction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var unit=other.GetComponentInParent<BaseUnit>();
            if (unit == null) {
                return;
            }
            approachObj = unit.gameObject;
            if (validate()) {
                trueAction.Invoke();
                gameObjectTrueAction.Invoke(approachObj);
            } 
        }
        private void OnTriggerExit2D(Collider2D other) {
            var unit = other.GetComponentInParent<BaseUnit>();
            if (unit == null) {
                return;
            }
            if (approachObj != null && approachObj.GetInstanceID() == unit.gameObject.GetInstanceID()) {
                if (validate()) {
                    falseAction.Invoke();
                    gameObjectFalseAction.Invoke(approachObj);
                }
                approachObj = null;
            }
        }
    }
}
