using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS.Unit {
    public class PickUpItemCreator : SerializedMonoBehaviour {
        public Vector2 vector;
        public System.Func<Vector2> vectorFunc;
        public string ID;

        public void create() {
            Vector2 vec = vector;
            if (vectorFunc != null) {
                vec = vectorFunc();
            }
            PickUpItemManager.instance.create(ID, vec);
        }

    }
}