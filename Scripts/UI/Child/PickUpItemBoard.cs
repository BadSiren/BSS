using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.UI {
    public class PickUpItemBoard : Board {
        private PickUpItem pickUpItem;
        private GameObject lastedObj;

        public void Show(string ID, PickUpItem _pickUpItem) {
            if (!Items.instance.database.ContainsKey(ID)) {
                return;
            }
            base.Show();
            pickUpItem = _pickUpItem;
            var item = Items.instance.database[ID];
            sendToReceiver("Icon", item.icon);
            sendToReceiver("Title", item.itemName);
            sendToReceiver("Content", item.itemDescription);
        }

        public void pickUp() {
            if (pickUpItem == null) {
                Close();
                return;
            }
            pickUpItem.pickUp();
            Close();
        }
    }

}
