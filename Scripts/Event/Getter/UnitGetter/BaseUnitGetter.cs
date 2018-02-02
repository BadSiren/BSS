using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class BaseUnitGetter : UnitGetter
    {
        public BaseUnit targetUnit {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponent<BaseUnit>();
            }
        }

        public string getName() {
            if (targetUnit==null) {
                return "";
            }
            return targetUnit.uName;
        }
        public Sprite getPortrait() {
            if (targetUnit==null) {
                return null;
            }
            return targetUnit.portrait;
        }
        public string getPlayerInfo() {
            if (targetUnit==null) {
                return "";
            }
            if (targetUnit.photonView.isSceneView) {
                return "중립";
            }
            return targetUnit.photonView.ownerId.ToString() + " Player";
        }

        public string getHealth() {
            if (targetUnit==null) {
                return "";
            }
            return targetUnit.health.ToString("0");
        }
        public string getMaxHealth() {
            if (targetUnit==null) {
                return "";
            }
            return targetUnit.maxHealth.ToString("0");
        }
        public string getArmorInfo() {
            if (targetUnit==null) {
                return "";
            }
            return StringUtil.valueToStringFloor(targetUnit.armor, targetUnit.initArmor);
        }
        public string getArmorContent() {
            if (targetUnit==null) {
                return "";
            }
            string text0 = "방어력: " + StringUtil.valueToString(targetUnit.armor, targetUnit.initArmor);
            string text1 = "피해감소율: " + StringUtil.valueToStringFloor(UnitUtils.GetDamageReduction(targetUnit.armor) * 100f, UnitUtils.GetDamageReduction(targetUnit.initArmor) * 100f) + "%";
            string text2 = "";
            var movable = targetUnit.GetComponent<Movable>();
            if (movable != null) {
                text2 = "이동속도 : " + StringUtil.valueToString(movable.speed, movable.initSpeed);
            }
            return text0 + "\n" + text1 + "\n" + text2;
        }
    }
}
