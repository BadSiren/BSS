using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public class Itemable : SerializedMonoBehaviour
	{


		[Range(0,8)]
		public int maxCount;
		public List<Item> items=new List<Item>();
		public Dictionary<string,float> properties=new Dictionary<string,float>();

		[BoxGroup("Event(GameObject)")]
		public string itemChangeEvent="ItemUpdate";

		[HideInInspector()]
		public BaseUnit owner;

		void Awake() {
			owner = GetComponent<BaseUnit> ();
		}
		void Start() {
			var container=owner.activables.getContainerOrCreate ("Item");
			for (int i = 0; i < maxCount; i++) {
                var actItem = container.AddComponent<ActItem>();
                actItem.isIgnore = true;
				owner.activables.registActivable ("Item",i,actItem);
			}
		}

		public void addItem(string ID) {
			if (items.Count >= maxCount) {
				return;
			}
			owner.photonView.RPC ("recvAddItem", PhotonTargets.All, ID);
		}
		public void throwItem(int index) {
			owner.photonView.RPC ("recvThrowItem", PhotonTargets.All, index);
		}
		public Item getItemOrNull(int index) {
			if (items.Count-1<index) {
				return null;
			}
			return items [index];
		}

		[PunRPC]
		private void recvAddItem(string ID) {
			items.Add (BSDatabase.instance.items.database [ID]);
			updateItem ();
		}
		[PunRPC]
		private void recvThrowItem(int index) {
			items.RemoveAt (index);
			updateItem ();
		}
		private void updateItem() {
			//Remove Property
			var applyComponents=GetComponents<IItemPropertyApply> ();
			foreach (var comp in applyComponents) {
				foreach (var property in properties) {
					comp.removeProperty (property.Key, property.Value);
				}
			}
			//Update Property
			updateProperties ();
			//Add Property
			foreach (var comp in applyComponents) {
				foreach (var property in properties) {
					comp.addProperty (property.Key, property.Value);
				}
			}

			BaseEventListener.onPublishGameObject (itemChangeEvent, gameObject, gameObject);
		}

		private void updateProperties() {
			properties.Clear ();
			foreach (var item in items) {
				foreach (var property in item.properties) {
					if (properties.ContainsKey (property.Key)) {
						properties [property.Key] = properties [property.Key] + property.Value;
					} else {
						properties [property.Key]=property.Value;
					}
				}
			}
		}

	}
}

