using UnityEngine;
using System.Collections;

namespace BSS.UI {
    public class TimerBoard : Board
    {
        public int timer = 3;

        public override void Show() {
            base.Show();
            Invoke("Close", timer);
        }
    }
}
