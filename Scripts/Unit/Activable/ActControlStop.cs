using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    public class ActControlStop : Activable {
        public override void initialize() {
        }
        public override void activate() {
            if (!checkInteractable()) {
                return;
            }

            var movable=owner.GetComponent<Movable>();
            if (movable!=null) {
                movable.followStop();
                movable.moveStop();
            }
            var attackable = owner.GetComponent<Attackable>();
            if (attackable != null) {
                attackable.huntStop();
            }
            activables.actSelect(-1);
        }

    }
}