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
        public string getAttackContent() {
            if (!existAttackable()) {
                return "";
            }
            string text0 = "공격력: " + StringUtil.valueToString(attackable.damage, attackable.initDamage);
            string text1 = "공격속도: " + StringUtil.valueToString(attackable.attackSpeed, attackable.initAttackSpeed);
            string text2 = "사거리: " + StringUtil.valueToString(attackable.range, attackable.initRange);
            return text0 + "\n" + text1 + "\n" + text2;
        }
    }

}
