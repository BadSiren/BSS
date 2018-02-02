using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
    public class Item {
        public bool consumable;
		public string ID;
        public string category;
		public string itemName;
		[TextArea()]
		public string itemDescription;
		public Sprite icon;
		public Dictionary<string,float> properties=new Dictionary<string,float>();
        public Dictionary<string,float> useActs = new Dictionary<string, float>();
	}
	public class Items : SerializedMonoBehaviour
	{
        public static Items instance;
		public Dictionary<string,Item> database=new Dictionary<string,Item>();


        void Awake() {
            if (instance == null) {
                instance = this;
            }
        }
	}
}

