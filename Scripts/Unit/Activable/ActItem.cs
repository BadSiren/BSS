using UnityEngine;
using System.Collections;
using BSS.UI;
using UnityEngine.EventSystems;
using BSS.Input;
using BSS.Event;

namespace BSS.Unit {
    public class ActItem : Activable, IInputReact {
		private Itemable itemable;
        private InItem item {
            get {
                if (itemable == null) {
                    return null;
                }
                return itemable.getItemOrNull(index);
            }
        }

		public override void initialize ()
		{
            itemable = owner.GetComponent<Itemable> ();
            Clickable.clickReactList.Add(this);
		}
        public override void deInitialize() {
            Clickable.clickReactList.Remove(this);
        }
		public override void activate() {
            if (item == null || !checkInteractable()) {
				return;
			}
            if (isSelected) {
                if (item.usable) {
                    itemable.useItem(index);
                }
                activables.actSelect(-1);
            } else {
                if (activables.selectedAct == -1) {
                    activables.actSelect(index);
                    BaseEventListener.onPublishGameObject("ItemSelect", owner.gameObject, owner.gameObject);
                } else {
                    itemable.swapItem(activables.selectedAct, index);
                    activables.actSelect(-1);
                }
            }
		}

		public override Sprite getIcon ()
		{
            if (item == null ) {
				return null;
			}
            return item.icon;
		}
		public override string getTitle ()
		{
            if (item == null) {
				return "";
			}
            if (item.consumable) {
                return item.itemName + " [" + itemable.items[index].count.ToString() + "]";
            }
            return item.itemName;
		}
		public override string getText ()
		{
            if (item == null) {
				return "";
			}
            return item.itemDescription;
		}


        //Interface
        public void onClick(string clickName, GameObject target) {
            if (!checkInteractable() || !BaseSelect.instance.isMainSelect(owner)) {
                return;
            }
            if (item==null || !isSelected || clickName != "MapOnce") {
                return;
            }
            /*
            PickUpItemManager.instance.create(item.ID, owner.transform.position);
            itemable.removeItem(index);
            activables.actSelect(-1);
            */
        }
    }
}

