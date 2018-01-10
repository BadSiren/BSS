using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public class Itemable : SerializedMonoBehaviour
	{
		public int maxCount;
		public List<Item> items=new List<Item>();
	}
}

