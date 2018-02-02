using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class UnitAttackableGetter : UnitGetter {
        public Attackable attackable {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponent<Attackable>();
            }
        }

        public string getAttackInfo() {
            if (attackable==null) {
                return "";
            }
            return StringUtil.valueToStringFloor(attackable.damage, attackable.initDamage);
        }
        public string getAttackContent() {
            if (attackable == null) {
                return "";
            }
            string text0 = "공격력: " + StringUtil.valueToString(attackable.damage, attackable.initDamage);
            string text1 = "공격속도: " + StringUtil.valueToString(attackable.attackSpeed, attackable.initAttackSpeed);
            string text2 = "사거리: " + StringUtil.valueToString(attackable.range, attackable.initRange);
            return text0 + "\n" + text1 + "\n" + text2;
        }

    }
}