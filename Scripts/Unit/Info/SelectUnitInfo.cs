using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    public class SelectUnitInfo : MonoBehaviour {
        public BaseUnit selectUnit {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.owner;
            }
        }
        public bool existSelectUnit() {
            return selectUnit != null;
        }

        public string getName() {
            if (!existSelectUnit()) {
                return "";
            }
            return selectUnit.uName;
        }
        public Sprite getPortrait() {
            if (!existSelectUnit()) {
                return null;
            }
            return selectUnit.portrait;
        }
        public string getHealth() {
            if (!existSelectUnit()) {
                return "";
            }
            return selectUnit.health.ToString("0");
        }
        public string getMaxHealth() {
            if (!existSelectUnit()) {
                return "";
            }
            return selectUnit.maxHealth.ToString("0");
        }
        public string getArmorInfo() {
            if (!existSelectUnit()) {
                return "";
            }
            return StringUtil.valueToStringFloor(selectUnit.armor, selectUnit.initArmor);
        }
    }
}
