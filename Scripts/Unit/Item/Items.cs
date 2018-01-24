using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
    public class Item {
		public string ID;
        public string category;
		public string itemName;
		[TextArea()]
		public string itemDescription;
		public Sprite icon;
		public Dictionary<string,float> properties=new Dictionary<string,float>();
	}
	public class Items : SerializedMonoBehaviour
	{
		public Dictionary<string,Item> database=new Dictionary<string,Item>();
	}
}

