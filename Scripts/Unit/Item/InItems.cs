using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {    
	public class InItems : SerializedMonoBehaviour
	{
        public static InItems instance;
		public Dictionary<string,InItem> database=new Dictionary<string,InItem>();
        public Dictionary<string, int> categoryMax = new Dictionary<string, int>();


        void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this);
                foreach (var item in GetComponentsInChildren<InItem>()) {
                    database.Add(item.ID, item);
                }
            } else {
                Destroy(gameObject);
            }
        }
	}
}

