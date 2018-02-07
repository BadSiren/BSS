using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace BSS.Unit {
    public class PickUpItemCreator : SerializedMonoBehaviour {
        public Vector2 vector;
        public System.Func<Vector2> vectorFunc;
        public int itemMaxCount=2;
        public Dictionary<string, float> itemProbability = new Dictionary<string, float>();

        public void create() {
            Vector2 vec = vector;
            if (vectorFunc != null) {
                vec = vectorFunc();
            }
            int count = 0;
            foreach (var it in itemProbability) {
                if (Random.value < it.Value ) {
                    if(count >= itemMaxCount) {
                        return;
                    }
                    vec += new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    PickUpItemManager.instance.create(it.Key, vec);
                    count++;
                }
            }

        }

    }
}