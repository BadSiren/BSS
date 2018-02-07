using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class SelectItemableInfo : MonoBehaviour {
        public Itemable itemable {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.gameObject.GetComponent<Itemable>();
            }
        }

        public bool existItemable() {
            return itemable != null;
        }
        public bool isSelected(int index) {
            return false;
            /*
            if (!existItemable()) {
                return false;
            }
            return itemable.selectedItem == index;
            */
        }
        public bool isSelected() {
            return false;
            /*
            if (!existItemable()) {
                return false;
            }
            return itemable.selectedItem != -1;
            */
        }
        public InItem getSelectedItem() {
            return null;
            //return itemable.getItemOrNull(itemable.selectedItem);
        }

        public string getTitle() {
            if (!existItemable() || !isSelected()) {
                return "";
            }
            var item = getSelectedItem();
            if (item == null) {
                return "";
            }
            return item.itemName;
        }
        public string getContent() {
            if (!existItemable() || !isSelected()) {
                return "";
            }
            var item = getSelectedItem();
            if (item == null) {
                return "";
            }
            return item.itemDescription;
        }
        public Sprite getIcon() {
            if (!existItemable() || !isSelected()) {
                return null;
            }
            var item = getSelectedItem();
            if (item == null) {
                return null;
            }
            return item.icon;
        }


    }
}
