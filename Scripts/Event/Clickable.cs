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
		[TabGroup("Vector2")]
		public Vector2Event vector2TrueAction;
		[TabGroup("Vector2")]
        public Vector2Event vector2FalseAction;


		public void onClick() {
			Vector2 mp=BSS.Input.BaseInput.getMousePoint ();
            if (validate()) {
                trueAction.Invoke();
                gameObjectTrueAction.Invoke(parent);
                vector2TrueAction.Invoke(mp);
            } else {
                falseAction.Invoke();
                gameObjectFalseAction.Invoke(parent);
                vector2FalseAction.Invoke(mp);
            }
		}

        public void onPublish(string clickName) {
            foreach (var it in clickReactList) {
                it.onClick(clickName, parent);
            }
        }

	}
}

