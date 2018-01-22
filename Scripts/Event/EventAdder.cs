using UnityEngine;
using System.Collections;

namespace BSS {
    public static class EventAdder
    {
        public static void createEvent(GameObject target,string eventName) {
            var listener=target.AddComponent<BaseEventListener>();
            listener.eventName = eventName;
        }
    }
}