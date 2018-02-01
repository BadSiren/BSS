using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace BSS.Event {
    public class BSEvent : SerializedMonoBehaviour
    {
        public static List<BSEvent> eventers= new List<BSEvent>();


        public bool hasParent;
        [ShowIf("hasParent")]
        public GameObject _parent;
        public GameObject parent {
            get {
                if (_parent == null) {
                    return gameObject;
                }
                return _parent;
            }
        }
        [FoldoutGroup("Condition")]
        public List<System.Func<bool>> trueConditions = new List<System.Func<bool>>();
        [FoldoutGroup("Condition")]
        public List<System.Func<bool>> falseConditions = new List<System.Func<bool>>();
        [TabGroup("Action")]
        public UnityEvent trueAction;
        [TabGroup("Action")]
        public UnityEvent falseAction;

        void Awake() {
            eventers.Add(this);
        }
        void OnDestroy()
        {
            eventers.Remove(this);
        }

        public virtual bool validate() {
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
            return true;
        }
    }
}
