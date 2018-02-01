using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS {
    public enum ParameterType {
        Void, GameObject, Int, String
    }
	public enum ESelectState {
		None,Mine,NotMine,Multi,
		All
	}
    public enum ESelectUnitState {
        None,Attack,Follow
    }
    public enum EClickType {
        Once, Double
    }
	public struct ArgWithRecevier
	{
		public string receiverName;
		public object parameter;
	}
	[System.Serializable]
	public class Vector2Event : UnityEvent<Vector2>{}
	[System.Serializable]
	public class GameObjectEvent : UnityEvent<GameObject>{}
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> {}

}

