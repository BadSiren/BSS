using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;
using BSS.UI;

namespace BSS {
	public class PickUpItem : MonoBehaviour
	{
        [SerializeField]
        private string _ID;
        public string ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }

        public void pickUp() {
            var itemable = BaseSelect.instance.mainSelectable.owner.GetComponent<Itemable>();
            if (itemable == null || !itemable.addItem(ID)) {
                return;
            }
            forceDestory();
        }
        public void destory() {
            var index = PickUpItemManager.instance.pickUpItems.FindIndex(x => x == this);
            if (index < 0) {
                return;
            }
            var item = InItems.instance.database[ID];
            if (item.isInvulnerable) {
                NotifyBoard.Notify("ItemCantDestroy");
                return;
            }
            PickUpItemManager.instance.destroy(index);
        }
        public void forceDestory() {
            var index = PickUpItemManager.instance.pickUpItems.FindIndex(x => x == this);
            if (index < 0) {
                return;
            }
            PickUpItemManager.instance.destroy(index);
        }
	}
}

