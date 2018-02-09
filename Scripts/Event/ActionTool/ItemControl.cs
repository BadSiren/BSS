using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS.Event {
    public class ItemControl : SerializedMonoBehaviour
    {
        [FoldoutGroup("TargetUnit")]
        public Itemable targetItemable;
        [FoldoutGroup("TargetUnit")]
        public System.Func<Itemable> itemableFunc;

        public virtual Itemable itemable {
            get {
                if (itemableFunc != null) {
                    return itemableFunc();
                }
                return targetItemable;
            }
        }

        public void toThrowSelectItem() {
            if (itemable==null || itemable.selectedItem==-1) {
                return;
            }
            var item=itemable.getItemOrNull(itemable.selectedItem);
            PickUpItemManager.instance.create(item.ID, itemable.owner.transform.position);
            itemable.removeItem(itemable.selectedItem);
            itemable.resetSelect();
        }
    }
}
