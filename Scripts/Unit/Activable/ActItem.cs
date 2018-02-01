﻿using UnityEngine;
using System.Collections;
using BSS.UI;
using UnityEngine.EventSystems;
using BSS.Input;
using BSS.Event;

namespace BSS.Unit {
    public class ActItem : Activable, IInputReact {
		private Itemable itemable;
        private Item item {
            get {
                if (itemable == null) {
                    return null;
                }
                return itemable.getItemOrNull(index);
            }
        }

		public override void initialize ()
		{
			itemable = GetComponentInParent<Itemable> ();
            Clickable.clickReactList.Add(this);
		}
        public override void deInitialize() {
            Clickable.clickReactList.Remove(this);
        }
		public override void activate() {
            if (item == null || !checkInteractable()) {
				return;
			}

            if (activables.selectedAct != -1) {
                if (activables.selectedAct != index) {
                    itemable.swapItem(activables.selectedAct, index);
                }
                activables.actSelect(-1);
                return;
            }
            activables.actSelect(-1);
		}

        public override void activateLongPress() {
            if (item == null || !checkInteractable()) {
                return;
            }
            activables.actSelect(index);
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
            if (!BaseSelect.instance.isMainSelect(owner)) {
                return;
            }
            if (item==null || !isSelected || clickName != "MapOnce") {
                return;
            }
            PickUpItemManager.instance.create(item.ID, owner.transform.position);
            itemable.removeItem(index);
            activables.actSelect(-1);
        }
    }
}

