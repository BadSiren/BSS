using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS {
	public class BaseEvent : MonoBehaviour
	{
		public static BaseEvent instance;
	
		public void Awake()
		{
			instance = this;
		}

		public void toMoveAllEvent(Vector3 targetPos) {
			foreach (var it in Movable.movableList) {
				if (it.canInputing) {
					it.toMove (targetPos);
				}
			}
		}
	}
}

