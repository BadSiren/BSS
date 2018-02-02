using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.UI {
    public class UpdateBoard : Board
    {
        public List<string> updatingEventList = new List<string>();
        public List<string> clearEventList = new List<string>();
        public List<string> showEventList = new List<string>();
        public List<string> closeEventList = new List<string>();

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
            foreach (var it in updatingEventList) {
                BaseEventListener.registEventer(it, gameObject, "updating");
            }
            foreach (var it in clearEventList) {
                BaseEventListener.registEventer(it, gameObject, "clear");
            }
            foreach (var it in showEventList) {
                BaseEventListener.registEventer(it, gameObject, "Show");
            }
            foreach (var it in closeEventList) {
                BaseEventListener.registEventer(it, gameObject, "Close");
            }
        }
        protected override void deInitialize() {
            base.deInitialize();
            BaseEventListener.unregistEventer(gameObject);
        }
        public override void Show() {
            base.Show();
            updating();
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