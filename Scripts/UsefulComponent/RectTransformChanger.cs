using UnityEngine;
using System.Collections;

namespace BSS {
    public class RectTransformChanger : MonoBehaviour
    {
        public RectTransform rectTr;
        public float posX;
        public float posY;

        private Vector2 prePos;

        void Start() {
            prePos = rectTr.localPosition;
        }

        public void rectChange()
        {
            rectTr.localPosition = prePos + new Vector2(posX, posY);
        }
        public void rectReset() {
            rectTr.localPosition = prePos;
        }
    }
}
