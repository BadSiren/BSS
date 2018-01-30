using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
    public class RandomGetter : MonoBehaviour {
        [FoldoutGroup("Vector")]
        public float minX;
        [FoldoutGroup("Vector")]
        public float maxX;
        [FoldoutGroup("Vector")]
        public float minY;
        [FoldoutGroup("Vector")]
        public float maxY;
        [FoldoutGroup("Float")]
        public float minFloat;
        [FoldoutGroup("Float")]
        public float maxFlaot;

        public Vector2 randVector2() {
            return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        public float randFloat() {
            return Random.Range(minFloat, maxFlaot);
        }
    }
}
