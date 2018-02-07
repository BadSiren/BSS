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

        public bool consumable;
        public Dictionary<string, float> properties = new Dictionary<string, float>();
        public Dictionary<string, float> useActs = new Dictionary<string, float>();
    }
}
