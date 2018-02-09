using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS {
    public class InItemActHealthRecovery : InItemAct
    {
        public int recoveryHealth;

        public override void activate(BaseUnit owner) {
            owner.health += recoveryHealth;
        }
    }
}
