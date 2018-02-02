using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Event {
    public class UnitActivablesGetter : UnitGetter {
        public Activables activables {
            get {
                if (target == null) {
                    return null;
                }
                return target.GetComponentInChildren<Activables>();
            }
        }

        public int getSelectedAct() {
            if (activables == null) {
                return -1;
            }
            return activables.selectedAct;
        }
        public void clearSelectAct() {
            if (activables == null) {
                return;
            }
            activables.actSelect(-1);
        }

        public string getActivableTitle(int index) {
            if (activables==null) {
                return "";
            }
            var activable = activables.getActivableOrNull(index);
            if (activable == null) {
                return "";
            }
            return activable.getTitle();
        }
        public Sprite getActivableIcon(int index) {
            if (activables == null) {
                return null;
            }
            var activable = activables.getActivableOrNull(index);
            if (activable == null) {
                return null;
            }
            return activable.getIcon();
        }

        public void activate(int index) {
            if (activables == null) {
                return;
            }
            var activable = activables.getActivableOrNull(index);
            if (activable == null) {
                return;
            }
            activable.activate();
        }
        public void activateLongPress(int index) {
            if (activables == null) {
                return;
            }
            var activable = activables.getActivableOrNull(index);
            if (activable == null) {
                return;
            }
            activable.activateLongPress();
        }
    }
}
