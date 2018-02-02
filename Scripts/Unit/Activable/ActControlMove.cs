using UnityEngine;
using System.Collections;
using BSS.Input;
using BSS.Event;

namespace BSS.Unit {
    public class ActControlMove : Activable,IInputReact
    {
        public GameObject pingEffect;
        private Movable movable;
        private Attackable attackable;

        public override void initialize() {
            movable = owner.GetComponent<Movable>();
            attackable = owner.GetComponent<Attackable>();
            Clickable.clickReactList.Add(this);
        }
        public override void deInitialize() {
            Clickable.clickReactList.Remove(this);
        }

        public override void activate() {
            if (movable == null || !checkInteractable()) {
                return;
            }
            if (activables.selectedAct == index) {
                activables.actSelect(-1);
                return;
            }
            activables.actSelect(index);
        }

        //Interface
        public void onClick(string clickName, GameObject target) {
            if (!checkInteractable() || !BaseSelect.instance.isMainSelect(owner) || movable == null ) {
                return;
            }
            if (clickName == "MapDouble" || (isSelected && clickName == "MapOnce")) {
                movable.followStop();
                if (attackable != null) {
                    attackable.huntStop();
                }
                movable.toMove(BaseInput.getMousePoint());
                if (pingEffect != null) {
                    Instantiate(pingEffect, BaseInput.getMousePoint(), Quaternion.identity);
                }
                activables.actSelect(-1);
            }
        }
    }
}
