using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BSS.Level {
	public class LevelEditor : MonoBehaviour
	{
		//LevelData
		[MenuItem("Assets/BSS/Create/Level/Database")]
		public static void CreateLevelDatabase ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<LevelDatabase> (_path);
			}
		}
	}
}

