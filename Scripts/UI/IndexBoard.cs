using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
    public class IndexBoard : Board
    {
        private List<ElementIndex> elementList = new List<ElementIndex>();

        protected override void initialize() {
            base.initialize();
            foreach (var it in GetComponentsInChildren<ElementIndex>()) {
                elementList.Add(it);
            }
        }

        public void updating() {
            foreach (var it in elementList) {
                it.updaing();
            }
        }

    }
}
