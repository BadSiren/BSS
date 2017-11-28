using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public class ActivableDatabase : SerializedScriptableObject
	{
		public Dictionary<string,Activable> activables=new Dictionary<string,Activable>();

		public T getActivable<T>(string _ID) where T : Activable{
			if (activables.ContainsKey (_ID)) {
				Debug.LogError ("No Activable");
			}
			return (activables [_ID] as T);
		}
		public T createActivable<T>(string _ID,Dictionary<string,string> args) where T : Activable{
			if (activables.ContainsKey (_ID)) {
				Debug.LogError ("No Activable");
			}
			var activable=ScriptableObject.Instantiate (activables [_ID]) as T;
			activable.initialize (args);
			return activable;
		}
	}
}

