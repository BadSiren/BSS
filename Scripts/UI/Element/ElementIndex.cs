using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace BSS.UI {
    public class ElementIndex : SerializedMonoBehaviour {
        public int index;
        public System.Func<int,bool> boolFunc;
        public System.Func<int,string> stringFunc;
        public System.Func<int,Sprite> spriteFunc;
        public bool clearIgnore = false;

        private UpdateBoard updateBoard;
        private Text textComp;
        private Image imageComp;

        void Awake() {
            updateBoard = GetComponentInParent<UpdateBoard>();
            textComp = GetComponent<Text>();
            imageComp = GetComponent<Image>();
        }

        public void updaing() {
            if (textComp != null && stringFunc != null) {
                textComp.text = stringFunc.Invoke(index);
            }
            if (imageComp != null && spriteFunc != null) {
                if (spriteFunc.Invoke(index) == null) {
                    imageComp.enabled = false;
                } else {
                    imageComp.enabled = true;
                    imageComp.sprite = spriteFunc.Invoke(index);
                }
            }
            if (boolFunc != null) {
                if (textComp != null) {
                    textComp.enabled = boolFunc(index);
                }
                if (imageComp != null) {
                    imageComp.enabled = boolFunc(index);
                }
            }
        }
        public void clear() {
            if (clearIgnore) {
                return;
            }
            if (textComp != null) {
                textComp.text = "";
            }
            if (imageComp != null) {
                imageComp.enabled = false;
            }
        }

    }
}

