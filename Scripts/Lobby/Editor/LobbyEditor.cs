using UnityEngine;
using System.Collections;
using UnityEditor;
using BSS.Play;

namespace BSS.LobbyItemSystem {
	public class LobbyEditor : MonoBehaviour
	{
		//Lobby Item
		[MenuItem("Assets/BSS/Create/LobbyItem/Database")]
		public static void CreateLobbyItemDatabase ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyItemDatabase> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/LobbyItem/EquipItem")]
		public static void CreateEquipItem ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				
				ScriptableObjectUtility.CreateAsset<LobbyEquipItem> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/LobbyItem/ConsumeItem")]
		public static void CreateConsumeItem ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyConsumeItem> (_path);
			}
		}

		//Lobby Act
		[MenuItem("Assets/BSS/Create/LobbyAct/Transport")]
		public static void CreateTransport ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyActTransport> (_path);
			}
		}

		//Play Act
		[MenuItem("Assets/BSS/Create/PlayAct/AddActivable")]
		public static void CreateAddActivable ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<AddActivable> (_path);
			}
		}
	}
}

