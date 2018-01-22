using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace BSS {
	public class Clickable : SerializedMonoBehaviour
	{
		public enum EClickType
		{
			Once,Double
		}
		public bool hasParent;
        [ShowIf("hasParent")]
        public GameObject parent;
		public EClickType eClickType;

		[FoldoutGroup("Condition(GameObject)")]
		public List<Condition> trueConditions=new List<Condition>();
		[FoldoutGroup("Condition(GameObject)")]
		public List<Condition> falseConditions=new List<Condition>();

		[TabGroup("Void")]
		public UnityEvent onClickTrueEvent;
		[TabGroup("Void")]
		public UnityEvent onClickFalseEvent;
		[TabGroup("GameObject")]
		public GameObjectEvent onGameObjectTrueEvent;
		[TabGroup("GameObject")]
		public GameObjectEvent onGameObjectFalseEvent;
		[TabGroup("Vector2")]
		public Vector2Event onVector2TrueEvent;
		[TabGroup("Vector2")]
		public Vector2Event onVector2FalseEvent;


		public void onClick() {
			GameObject obj = gameObject;
			Vector2 mp=BSS.Input.BaseInput.getMousePoint ();
			if (hasParent) {
                obj=parent;
			}
			foreach (var it in trueConditions) {
				if (!it.validate (obj)) {
					onClickFalseEvent.Invoke ();
					onVector2FalseEvent.Invoke (mp);
					onGameObjectFalseEvent.Invoke (obj);
					return;
				}
			}
			foreach (var it in falseConditions) {
				if (it.validate (obj)) {
					onClickFalseEvent.Invoke ();
					onVector2FalseEvent.Invoke (mp);
					onGameObjectFalseEvent.Invoke (obj);
					return;
				}
			}
			onClickTrueEvent.Invoke ();
			onVector2TrueEvent.Invoke (mp);
			onGameObjectTrueEvent.Invoke (obj);
		}

	}
}

