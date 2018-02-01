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
		private Item item;

		void Start() {
            item=Items.instance.database [ID];
		}

        public void pickUp() {
            var itemable = BaseSelect.instance.mainSelectable.owner.GetComponent<Itemable>();
            if (itemable == null) {
                return;
            }
            itemable.addItem(ID);
            var index=PickUpItemManager.instance.pickUpItems.FindIndex(x => x == this);
            if (index < 0) {
                Debug.Log("No Find");
                return;
            }
            PickUpItemManager.instance.destroy(index);
        }
	}
}

