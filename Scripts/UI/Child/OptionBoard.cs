using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System;

namespace BSS.UI {
    public class OptionBoard : Board
    {
        public int optionCount = 4;
        public Action action0;
        public Action action1;
        public Action action2;

        public override void Show() {
            base.Show();
            sendBoolToReceiver("Title", false);
            for (int i = 0; i < optionCount; i++) {
                sendBoolToReceiver("Option" + i.ToString(), false);
            }
        }
        public void Show(string mainTitle,string title0,Action act0) {
            Show();
            sendToReceiver("Title", "["+mainTitle+"]");
            sendBoolToReceiver("Option0", true);
            sendToReceiver("Option0", title0);
            action0 = act0;
        }
        public void Show(string mainTitle, string title0, Action act0,string title1,Action act1) {
            Show();
            sendToReceiver("Title", "[" + mainTitle + "]");
            sendBoolToReceiver("Option0", true);
            sendToReceiver("Option0", title0);
            action0 = act0;
            sendBoolToReceiver("Option1", true);
            sendToReceiver("Option1", title1);
            action1 = act1;
        }
        public void Show(string mainTitle, string title0, Action act0, string title1, Action act1,string title2,Action act2) {
            Show();
            sendToReceiver("Title", "[" + mainTitle + "]");
            sendBoolToReceiver("Option0", true);
            sendToReceiver("Option0", title0);
            action0 = act0;
            sendBoolToReceiver("Option1", true);
            sendToReceiver("Option1", title1);
            action1 = act1;
            sendBoolToReceiver("Option2", true);
            sendToReceiver("Option2", title2);
            action2 = act2;
        }

        public void selectOption(int index) {
            switch (index) {
                case 0:
                    if (action0 != null) {
                        action0();
                    }
                    Close();
                    break;
                case 1:
                    if (action1 != null) {
                        action1();
                    }
                    Close();
                    break;
                case 2:
                    if (action2 != null) {
                        action2();
                    }
                    Close();
                    break;
                default:
                    Close();
                    break;
            }
        }
    }
}