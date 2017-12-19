using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyEquipItem : LobbyItem
	{
		public string containerName;

		/*
		public void propertiesRand(string randKey) {
			foreach (var property in properties) {
				if (!property.Value.ContainsKey (randKey)) {
					continue;
				}
				string[] randSplit = property.Value [randKey].Split ('/');
				float randFloat = float.Parse (randSplit [1]) + Random.Range (int.Parse (randSplit [2]), int.Parse (randSplit [3])) * float.Parse (randSplit [4]);
				property.Value [randSplit [0]] = randFloat.ToString ();
				property.Value.Remove (randKey);
			}
		}
		*/
	}
}

