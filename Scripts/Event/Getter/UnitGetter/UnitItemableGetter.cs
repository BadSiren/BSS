using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class UnitItemableGetter : UnitGetter {
        public Itemable itemable {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponent<Itemable>();
            }
        }
        public Activables activables {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponentInChildren<Activables>();
            }
        }

        public string getSelectTitle() {
            if (itemable == null || activables==null || activables.selectedAct==-1) {
                return "";
            }
            var item = itemable.getItemOrNull(activables.selectedAct);
            if (item == null) {
                return "";
            }
            return item.itemName;
        }
        public string getTitle(int index) {
            if (itemable==null) {
                return "";
            }
            var item = itemable.getItemOrNull(index);
            if (item == null) {
                return "";
            }
            return item.itemName;
        }
        public string getSelectContent() {
            if (itemable == null || activables == null || activables.selectedAct == -1) {
                return "";
            }
            var item = itemable.getItemOrNull(activables.selectedAct);
            if (item == null) {
                return "";
            }
            return item.itemDescription;
        }
        public string getContent(int index) {
            if (itemable == null) {
                return "";
            }
            var item = itemable.getItemOrNull(index);
            if (item == null) {
                return "";
            }
            return item.itemDescription;
        }
        public Sprite getIcon(int index) {
            if (itemable == null) {
                return null;
            }
            var item = itemable.getItemOrNull(index);
            if (item == null) {
                return null;
            }
            return item.icon;
        }
        public Sprite getSelectIcon() {
            if (itemable == null || activables == null || activables.selectedAct == -1) {
                return null;
            }
            var item = itemable.getItemOrNull(activables.selectedAct);
            if (item == null) {
                return null;
            }
            return item.icon;
        }
    }
}