using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
    public class InItem : SerializedMonoBehaviour {
        public string ID;
        public string category;
        public string itemName;
        [TextArea()]
        public string itemDescription;
        public Sprite icon;

        public bool isInvulnerable;//DontDestroy
        public bool usable;
        public bool consumable;
        public Dictionary<string, float> properties = new Dictionary<string, float>();
        [HideInInspector]
        public List<InItemAct> useActs = new List<InItemAct>();

        void Start() {
            foreach (var it in GetComponents<InItemAct>()) {
                useActs.Add(it);
            }
        }
    }
}
