using UnityEngine;
using System.Collections;
using BSS.Input;
using BSS.Event;

namespace BSS.Unit {
    public class ActControlAttack : Activable, IInputReact {
        public GameObject pingEffect;
        private Attackable attackable;
        private Movable movable;

        public override void initialize() {
            attackable = owner.GetComponent<Attackable>();
            movable = owner.GetComponent<Movable>();
            Clickable.clickReactList.Add(this);
        }
        public override void deInitialize() {
            Clickable.clickReactList.Remove(this);
        }

        public override void activate() {
            if (attackable == null || !checkInteractable()) {
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
            if (!checkInteractable() ||!BaseSelect.instance.isMainSelect(owner) || attackable == null) {
                return;
            }
            var targetUnit = target.GetComponent<BaseUnit>();
            if (targetUnit == null || targetUnit.onlyMine) {
                return;
            }

            if (isSelected && clickName == "UnitOnce") {
                if (pingEffect != null) {
                    Instantiate(pingEffect, target.transform.position, Quaternion.identity);
                }
                if (movable != null) {
                    movable.toFollow(target, attackable.range * 0.9f);
                }
                attackable.toHunt(target);

                activables.actSelect(-1);
            }
        }
    }
}
