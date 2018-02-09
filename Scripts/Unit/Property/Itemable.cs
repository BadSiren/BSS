using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.UI;

namespace BSS.Unit {
    [System.Serializable]
    public class Itemable : SerializedMonoBehaviour
	{
        [System.Serializable]
        public class ItemInfo {
            public ItemInfo() {
            }
            public ItemInfo(string _ID, int _count) {
                ID = _ID;
                count = _count;
            }
            public string ID;
            public int count;
        }
		[Range(0,8)]
		public int maxCount;
        public List<ItemInfo> items=new List<ItemInfo>();
		public Dictionary<string,float> properties=new Dictionary<string,float>();

        public int selectedItem {
            get {
                if (owner.activables.nowCategory != "Item") {
                    return -1;
                }
                return owner.activables.selectedAct;
            }
        }

        [Header("GameObject")]
        [FoldoutGroup("BaseEvent")]
		public string itemChangeEvent="ItemUpdate";
        [FoldoutGroup("NotifyEvent")]
        public string fullCategoryNotify="FullCategory";

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


        public InItem getItemOrNull(int index) {
            if (items.Count - 1 < index) {
                return null;
            }
            return InItems.instance.database[items[index].ID];
        }
        public InItem getItem(string ID) {
            return InItems.instance.database[ID];
        }

		public bool addItem(string ID) {
            if (!owner.isMine || items.Count >= maxCount) {
                return false;
            }
            var item = getItem(ID);
            if (!categoryValidate(item.category)) {
                NotifyBoard.Notify(fullCategoryNotify);
                return false;
            }
            if (item.consumable) {
                int num = items.FindIndex(x => x.ID == ID);
                if (num >=0) {
                    items[num].count += 1;
                    owner.photonView.RPC("recvUpdateItems", PhotonTargets.All, serialize(items));
                    return true;
                };
            }
            items.Add(new ItemInfo(ID, 1));
            owner.photonView.RPC ("recvUpdateItems", PhotonTargets.All, serialize(items));
            return true;
		}
		public void removeItem(int index) {
            if (!owner.isMine || items.Count-1 < index) {
                return;
            }
            if (items[index].count > 1) {
                items[index].count -= 1;
            } else {
                items.RemoveAt(index);
            }
            owner.photonView.RPC ("recvUpdateItems", PhotonTargets.All, serialize(items));
		}
        public void swapItem(int index1,int index2) {
            if (!owner.isMine || index1==index2) {
                return;
            }
            var temp = items[index1];
            items[index1] = items[index2];
            items[index2] = temp;
            owner.photonView.RPC("recvUpdateItems", PhotonTargets.All, serialize(items));
        }
        public void useItem(int index) {
            if (!owner.isMine || items.Count >= maxCount) {
                return;
            }
            var item = getItem(items[index].ID);
            foreach (var it in item.useActs) {
                it.activate(owner);
            }
            if (item.consumable) {
                removeItem(index);
            }
        }
        public void resetSelect(){
            if (owner.activables.nowCategory != "Item") {
                return;
            }
            owner.activables.actSelect(-1);
        }


        [PunRPC]
        void recvUpdateItems(string code,PhotonMessageInfo mi) {
            items=deserialize(code);
            canclePropeties();
            setThisProperties();
            applyPropeties();
            var updateReacts = GetComponentsInChildren<IItemUpdateReact>();
            foreach (var it in updateReacts) {
                it.onItemUpdate();
            }
            BaseEventListener.onPublishGameObject(itemChangeEvent, gameObject, gameObject);
        }

        private bool categoryValidate(string category) {
            if (string.IsNullOrEmpty(category) || !InItems.instance.categoryMax.ContainsKey(category)) {
                return true;
            }
            var max = InItems.instance.categoryMax[category];
            int cnt = 0;
            for (int i = 0; i < items.Count; i++) {
                var item = getItemOrNull(i);
                if (category == item.category) {
                    cnt++;
                }
            }
            return cnt < max;
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
                var item = getItem(it.ID);
				foreach (var property in item.properties) {
					if (properties.ContainsKey (property.Key)) {
						properties [property.Key] = properties [property.Key] + property.Value;
					} else {
						properties [property.Key]=property.Value;
					}
				}
			}
		}

        private string serialize(List<ItemInfo> itemInfos) {
            string text = itemInfos.Count.ToString() + "/";
            foreach (var it in itemInfos) {
                text += it.ID + "/" + it.count.ToString() + "/";
            }
            return text;
        }
        private List<ItemInfo> deserialize(string code) {
            var itemInfos = new List<ItemInfo>();
            var codes = code.Split('/');
            int num = int.Parse(codes[0]);
            for (int i = 0; i < num; i++) {
                var info = new ItemInfo();
                info.ID = codes[2 * i + 1];
                info.count = int.Parse(codes[2 * i + 2]);
                itemInfos.Add(info);
            }
            return itemInfos;
        }
	}
}

