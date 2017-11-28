using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS {
	public struct ArgWithRecevier
	{
		public string receiverName;
		public object parameter;
	}
	public struct MessageArgsTwo
	{
		public object parameter0;
		public object parameter1;
	}
	public class MessageCallback
	{
		public string EventType;
		public object callback;
	}

	//SendMessage =>This gameObject is all class.
	//Publisher =>All gameObject is static class.
	[System.Serializable]
	public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
	{
		[SerializeField]
		List<TKey> keys;
		[SerializeField]
		List<TValue> values;

		Dictionary<TKey, TValue> target;
		public Dictionary<TKey, TValue> ToDictionary() { return target; }

		public Serialization(Dictionary<TKey, TValue> target)
		{
			this.target = target;
		}

		public void OnBeforeSerialize()
		{
			keys = new List<TKey>(target.Keys);
			values = new List<TValue>(target.Values);
		}

		public void OnAfterDeserialize()
		{
			var count = System.Math.Min(keys.Count, values.Count);
			target = new Dictionary<TKey, TValue>(count);
			for (var i = 0; i < count; ++i)
			{
				target.Add(keys[i], values[i]);
			}
		}
	}

	public class Define : MonoBehaviour 
	{

	}
}

