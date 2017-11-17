using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyEquipItem : LobbyItem
	{
		public string equipType;
		[MenuItem("Assets/BSS/Create/LobbyEquipItem")]
		public static void CreateLobbyEquipItem ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create LobbyEqipItem", "Assets/BSS/CustomAsset/LobbyItem/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyEquipItem> (_path);
			}
		}
	}
}

