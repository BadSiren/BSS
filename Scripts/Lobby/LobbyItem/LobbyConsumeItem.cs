using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BSS.LobbyItemSystem {
	[System.Serializable]
	public class LobbyConsumeItem : LobbyItem
	{
		[MenuItem("Assets/BSS/Create/LobbyConsumeItem")]
		public static void CreateLobbyEquipItem ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create LobbyConsumeItem", "Assets/BSS/CustomAsset/LobbyItem/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyConsumeItem> (_path);
			}
		}
	}
}

