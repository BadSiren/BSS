using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace BSS.Unit {
    public class PickUpItemCreator : SerializedMonoBehaviour {
        public Vector2 vector;
        public System.Func<Vector2> vectorFunc;
        public Dictionary<string, int> itemProbability = new Dictionary<string, int>();

        public void create() {
            Vector2 vec = vector;
            if (vectorFunc != null) {
                vec = vectorFunc();
            }
            int sum = 0;
            foreach (var it in itemProbability) {
                sum += it.Value;

            }
            var rand=Random.Range(0, sum);
            sum = 0;
            foreach (var it in itemProbability) {
                sum += it.Value;
                if (rand < sum) {
                    vec += new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    PickUpItemManager.instance.create(it.Key, vec);
                    return;
                }
            }

        }

    }
}