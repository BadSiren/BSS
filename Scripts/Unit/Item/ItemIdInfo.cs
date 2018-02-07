using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
    public class ItemIdInfo : SerializedMonoBehaviour
    {
        public string ID;
        public System.Func<string> idFunc;

        private string _ID {
            get {
                if (idFunc != null) {
                    return idFunc();
                }
                return ID;
            }
        }

        private InItem _item;
        private InItem item {
            get {
                if (InItems.instance.database.TryGetValue(_ID,out _item)) {
                    return _item;
                }
                return null;
            }
        }

        public string getItemName() {
            if (item == null) {
                return "";
            }
            return item.itemName;
        }
        public string getItemDescription() {
            if (item == null) {
                return "";
            }
            return item.itemDescription;
        }
    }
}