using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyEquipItem : LobbyItem
	{
		public string containerName;
		public Dictionary<string,Dictionary<string,string>> properties=new Dictionary<string,Dictionary<string,string>>();

		public void propertiesRand() {
			foreach (var property in properties) {
				if (!property.Value.ContainsKey ("Rand")) {
					continue;
				}
				string[] randSplit = property.Value ["Rand"].Split ('/');
				float randFloat = float.Parse (randSplit [1]) + Random.Range (int.Parse (randSplit [2]), int.Parse (randSplit [3])) * float.Parse (randSplit [4]);
				property.Value [randSplit [0]] = randFloat.ToString ();
				property.Value.Remove ("Rand");
			}
		}
	}
}

