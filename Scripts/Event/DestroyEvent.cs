using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace BSS.Event {
    public class DestroyEvent : BSEvent,IDieReact
    {

        public void onDie()
        {
            if (validate()) {
                trueAction.Invoke();
            }
        }
    }
}
