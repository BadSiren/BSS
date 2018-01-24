using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    public class SelectActivablesInfo : MonoBehaviour {
        public Activables activables {
            get {
                if (BaseSelect.instance.mainSelectable == null) {
                    return null;
                }
                return BaseSelect.instance.mainSelectable.owner.activables;
            }
        }
        public bool existActivables() {
            return activables != null;
        }

        public string getActivableTitle(int index) {
            if (!existActivables()) {
                return "";
            }
            var activable=activables.getActivableOrNull(index);
            if (activable==null) {
                return "";
            }
            return activable.getTitle();
        }
        public Sprite getActivableIcon(int index) {
            if (!existActivables()) {
                return null;
            }
            var activable = activables.getActivableOrNull(index);
            if (activable == null) {
                return null;
            }
            return activable.getIcon();
        }
        public void activate(int index) {
            if (!existActivables()) {
                return;
            }
            var activable = activables.getActivableOrNull(index);
            if (activable == null) {
                return;
            }
            activable.activate();
        }
        public void activateLongPress(int index) {
            if (!existActivables()) {
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
