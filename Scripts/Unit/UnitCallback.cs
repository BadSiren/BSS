using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.Callback {
    public class UnitCallback : BSCallback
    {
        public static List<UnitCallback> unitCallbacks = new List<UnitCallback>();

        void Awake() {
            unitCallbacks.Add(this);
        }
        void OnDestroy()
        {
            unitCallbacks.Remove(this);
        }

        public static void OnCreateUnit(BaseUnit unit) {
            foreach (var callback in unitCallbacks) {
                foreach (var listen in callback.listeners) {
                    listen.SendMessage("OnCreateUnit",unit, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        public static void OnCreateUnitOnlyMine(BaseUnit unit) {
            foreach (var callback in unitCallbacks) {
                foreach (var listen in callback.listeners) {
                    listen.SendMessage("OnCreateUnitOnlyMine", unit, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        public static void OnDestroyUnit(BaseUnit unit) {
            foreach (var callback in unitCallbacks) {
                foreach (var listen in callback.listeners) {
                    listen.SendMessage("OnDestroyUnit", unit, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        public static void OnDestroyUnitOnlyMine(BaseUnit unit) {
            foreach (var callback in unitCallbacks) {
                foreach (var listen in callback.listeners) {
                    listen.SendMessage("OnDestroyUnitOnlyMine", unit, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
