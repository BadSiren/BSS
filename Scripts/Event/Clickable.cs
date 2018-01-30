using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace BSS.Event {
    public class Clickable : BSEvent
	{
		public enum EClickType
		{
			Once,Double
		}
		public bool hasParent;
        [ShowIf("hasParent")]
        public GameObject parent;
		public EClickType eClickType;
        public bool isPublish;
        [ShowIf("isPublish")]
        public string publishKey;

		[TabGroup("GameObject")]
		public GameObjectEvent gameObjectTrueAction;
		[TabGroup("GameObject")]
        public GameObjectEvent gameObjectFalseAction;
		[TabGroup("Vector2")]
		public Vector2Event vector2TrueAction;
		[TabGroup("Vector2")]
        public Vector2Event vector2FalseAction;


		public void onClick() {
			GameObject obj = gameObject;
			Vector2 mp=BSS.Input.BaseInput.getMousePoint ();
			if (hasParent) {
                obj=parent;
			}
            if (validate()) {
                trueAction.Invoke();
                gameObjectTrueAction.Invoke(obj);
                vector2TrueAction.Invoke(mp);
            } else {
                falseAction.Invoke();
                gameObjectFalseAction.Invoke(obj);
                vector2FalseAction.Invoke(mp);
            }
		}
        public void onPublish() {
            BSS.Input.BaseInput.instance.onPublish(publishKey);
        }


	}
}

