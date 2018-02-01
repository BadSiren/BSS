using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.UI;

namespace BSS.Unit {
    [System.Serializable]
    public class Itemable : SerializedMonoBehaviour,ISelectReact
	{
		[Range(0,8)]
		public int maxCount;
		public List<string> items=new List<string>();
		public Dictionary<string,float> properties=new Dictionary<string,float>();

        [Header("GameObject")]
        [FoldoutGroup("BaseEvent")]
		public string itemChangeEvent="ItemUpdate";
        [FoldoutGroup("BaseEvent")]
        public string itemSelectedEvent = "ItemSelected";
        [FoldoutGroup("BaseEvent")]
        public string itemDeselectedEvent = "ItemDeselected";

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


        public Item getItemOrNull(int index) {
            if (items.Count - 1 < index) {
                return null;
            }
            return Items.instance.database[items[index]];
        }
        public Item getItem(string ID) {
            return Items.instance.database[ID];
        }

		public void addItem(string ID) {
            if (!owner.isMine|| items.Count >= maxCount) {
				return;
			}
            items.Add(ID);
            owner.photonView.RPC ("recvUpdateItems", PhotonTargets.All, itemSerialize());
		}
		public void removeItem(int index) {
            if (!owner.isMine) {
                return;
            }
            items.RemoveAt(index);
            owner.photonView.RPC ("recvUpdateItems", PhotonTargets.All, itemSerialize());
		}
        public void swapItem(int index1,int index2) {
            if (!owner.isMine || index1==index2) {
                return;
            }
            var temp = items[index1];
            items[index1] = items[index2];
            items[index2] = temp;
            owner.photonView.RPC("recvUpdateItems", PhotonTargets.All, itemSerialize());
        }

        [PunRPC]
        void recvUpdateItems(string code,PhotonMessageInfo mi) {
            itemDeserialize(code);
            canclePropeties();
            setThisProperties();
            applyPropeties();
            var updateReacts = GetComponentsInChildren<IItemUpdateReact>();
            foreach (var it in updateReacts) {
                it.onItemUpdate();
            }
            BaseEventListener.onPublishGameObject(itemChangeEvent, gameObject, gameObject);
        }

		
        private void applyPropeties() {
            var applyComponents = GetComponents<IItemPropertyApply>();
            foreach (var comp in applyComponents) {
                foreach (var property in properties) {
                    comp.applyProperty(property.Key, property.Value);
                }
            }
        }
        private void canclePropeties() {
            var applyComponents = GetComponents<IItemPropertyApply>();
            foreach (var comp in applyComponents) {
                foreach (var property in properties) {
                    comp.cancleProperty(property.Key, property.Value);
                }
            }
        }
		private void setThisProperties() {
			properties.Clear ();
			foreach (var it in items) {
                var item = getItem(it);
				foreach (var property in item.properties) {
					if (properties.ContainsKey (property.Key)) {
						properties [property.Key] = properties [property.Key] + property.Value;
					} else {
						properties [property.Key]=property.Value;
					}
				}
			}
		}
        private string itemSerialize() {
            string text = items.Count.ToString()+"/";
            foreach (var it in items) {
                text += it+"/";
            }
            return text;
        }
        private void itemDeserialize(string code) {
            items.Clear();
            var codes=code.Split('/');
            int num=int.Parse(codes[0]);
            for (int i = 0; i < num; i++) {
                items.Add(codes[i + 1]);
            }
        }

        //Interface
        public void onSelect() {
        }
        public void onDeselect() {
        }
	}
}

