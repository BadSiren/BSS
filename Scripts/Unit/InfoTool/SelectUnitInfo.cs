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
        public GameObject getSelectGameObject() {
            if (selectUnit == null) {
                return null;
            }
            return selectUnit.gameObject;
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
        public string getPlayerInfo() {
            if (!existSelectUnit()) {
                return "";
            }
            if (selectUnit.photonView.isSceneView) {
                return "중립";
            }
            return selectUnit.photonView.ownerId.ToString() + " Player";
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
        public string getArmorContent() {
            if (!existSelectUnit()) {
                return "";
            }
            string text0 = "방어력: " + StringUtil.valueToString(selectUnit.armor, selectUnit.initArmor);
            string text1 = "피해감소율: " + StringUtil.valueToStringFloor(UnitUtils.GetDamageReduction(selectUnit.armor) * 100f, UnitUtils.GetDamageReduction(selectUnit.initArmor) * 100f) + "%";
            string text2 = "";
            var movable = selectUnit.GetComponent<Movable>();
            if (movable != null) {
                text2 = "이동속도 : " + StringUtil.valueToString(movable.speed, movable.initSpeed);
            }
            return text0 + "\n" + text1 + "\n" + text2;
        }
    }
}
