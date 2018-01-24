using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    public class SelectAttackableInfo : MonoBehaviour
    {
        public Attackable attackable {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.owner.GetComponent<Attackable>();
            }
        }
        public bool existAttackable() {
            return attackable != null;
        }

        public string getAttackInfo() {
            if (!existAttackable()) {
                return "";
            }
            return StringUtil.valueToStringFloor(attackable.damage, attackable.initDamage);
        }
    }

}
