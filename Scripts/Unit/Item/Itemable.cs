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

		public BaseUnit owner;

		void Awake() {
			owner = GetComponent<BaseUnit> ();
		}
		[PunRPC]
		private void recvAddItem(string ID) {
			if (items.Count >= maxCount) {
				return;
			}
			var container=owner.activables.getContainerOrCreate ("Item");
			var actItem=container.AddComponent<ActItem> ();
			actItem.category = "Item";
			actItem.index = items.Count;
			actItem.ID = ID;
			owner.activables.registActivable (actItem);
			items.Add (BSDatabase.instance.items.database [ID]);
		}
		[PunRPC]
		private void recvThrowItem(int index) {
			if (items.Count-1 < index) {
				return;
			}
			owner.activables.unregistActivable ("Item",index);
			PickUpItem.Create (items [index].ID, owner.transform.position);
			items.RemoveAt (index);
		}
	}
}

