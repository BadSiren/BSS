using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
    public class UpdateBoard : Board
    {
        private List<ElementVoid> elementVoidList = new List<ElementVoid>();
        private List<ElementIndex> elementIndexList = new List<ElementIndex>();

        protected override void initialize() {
            base.initialize();
            foreach (var it in GetComponentsInChildren<ElementVoid>()) {
                elementVoidList.Add(it);
            }
            foreach (var it in GetComponentsInChildren<ElementIndex>()) {
                elementIndexList.Add(it);
            }
        }

        public void updating() {
            foreach (var it in elementVoidList) {
                it.updaing();
            }
            foreach (var it in elementIndexList) {
                it.updaing();
            }
        }
        public void clear() {
            foreach (var it in elementVoidList) {
                it.clear();
            }
            foreach (var it in elementIndexList) {
                it.clear();
            }
        }
    }
}