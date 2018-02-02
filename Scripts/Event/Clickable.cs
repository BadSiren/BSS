using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using BSS.Input;

namespace BSS.Event {
    public class Clickable : BSEvent
	{
        public static List<IInputReact> clickReactList = new List<IInputReact>();

		public EClickType eClickType;

		[TabGroup("GameObject")]
		public GameObjectEvent gameObjectTrueAction;
		[TabGroup("GameObject")]
        public GameObjectEvent gameObjectFalseAction;


		public void onClick() {
            if (validate()) {
                trueAction.Invoke();
                gameObjectTrueAction.Invoke(parent);
            } else {
                falseAction.Invoke();
                gameObjectFalseAction.Invoke(parent);
            }
		}

        public void onPublish(string clickName) {
            foreach (var it in clickReactList) {
                it.onClick(clickName, parent);
            }
        }

	}
}

