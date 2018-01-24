using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace BSS.UI {
    public class ElementVoid : SerializedMonoBehaviour {
        public System.Func<bool> boolFunc;
        public System.Func<string> stringFunc;
        public System.Func<Sprite> spriteFunc;
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
                textComp.text = stringFunc.Invoke();
            }
            if (imageComp != null && spriteFunc != null) {
                if (spriteFunc.Invoke()==null) {
                    imageComp.enabled=false;
                } else {
                    imageComp.enabled = true;
                    imageComp.sprite = spriteFunc.Invoke();
                }
            }
            if (boolFunc!=null) {
                if (textComp != null) {
                    textComp.enabled = boolFunc();
                }
                if (imageComp != null) {
                    imageComp.enabled = boolFunc();
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

