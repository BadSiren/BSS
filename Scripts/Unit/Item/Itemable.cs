using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public class Itemable : SerializedMonoBehaviour
	{
		[Range(0,8)]
		public int maxCount;
		public List<Item> items=new List<Item>();
		public List<ActItem> actItemList=new List<ActItem>();

		public BaseUnit owner;
		private GameObject actContainer;

		void Awake() {
			owner = GetComponent<BaseUnit> ();
			actContainer = transform.Find ("Activable").gameObject;
		}
		[PunRPC]
		public void recvAddItem(string ID) {
			if (items.Count >= maxCount) {
				return;
			}
			var actItem=actContainer.AddComponent<ActItem> ();
			actItem.category = "Item";
			actItem.ID = ID;
			actItemList.Add (actItem);
			items.Add (BSDatabase.instance.items.database [ID]);
		}
	}
}

