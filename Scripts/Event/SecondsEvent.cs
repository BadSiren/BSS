using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace BSS.Event {
    public class SecondsEvent : BSEvent {

        public bool isDynamic;
        [HideIf("isDynamic")]
        public float seconds = 1f;
        [ShowIf("isDynamic")]
        public float minSeconds = 1f;
        [ShowIf("isDynamic")]
        public float maxSeconds = 3f;

        private float _seconds {
            get {
                if (isDynamic) {
                    return Random.Range(minSeconds, maxSeconds);
                }
                return seconds;
            }
        }

        void Start() {
            StartCoroutine(coroutine());
        }

        IEnumerator coroutine() {
            while (true) {
                yield return new WaitForSeconds(_seconds);
                if (validate()) {
                    trueAction.Invoke();
                } else {
                    falseAction.Invoke();
                }
            }
        }
    }
}
