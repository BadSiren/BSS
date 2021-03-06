﻿using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BSS.LobbyItemSystem {
	public class LobbyEditor : MonoBehaviour
	{
		//Lobby Item
		[MenuItem("Assets/BSS/Create/LobbyItem/Database")]
		public static void CreateLobbyItemDatabase ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyItemDatabase> (_path);

			}
		}
		[MenuItem("Assets/BSS/Create/LobbyItem/EquipItem")]
		public static void CreateEquipItem ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				
				ScriptableObjectUtility.CreateAsset<LobbyEquipItem> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/LobbyItem/ConsumeItem")]
		public static void CreateConsumeItem ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyConsumeItem> (_path);
			}
		}

		//Lobby Act
		[MenuItem("Assets/BSS/Create/LobbyAct/Transport")]
		public static void CreateTransport ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LobbyActTransport> (_path);
			}
		}

		//EquipProperty
		[MenuItem("Assets/BSS/Create/EquipProperty/AddActivable")]
		public static void CreateAddActivable ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<AddActivable> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/EquipProperty/AddHealth")]
		public static void CreateAddHealth ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<AddHealth> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/EquipProperty/AddDamage")]
		public static void CreateAddDamage ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<AddDamage> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/EquipProperty/AddMoney")]
		public static void CreateAddMoney ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<AddMoney> (_path);
			}
		}
	}
}

