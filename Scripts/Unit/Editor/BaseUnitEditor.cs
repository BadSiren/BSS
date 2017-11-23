using UnityEngine;
using System.Collections;
using UnityEditor;
using BSS.Play;

namespace BSS.Unit {
	public class BaseUnitEditor : MonoBehaviour
	{
		//BaseUnit
		[MenuItem("Assets/BSS/Create/Unit/Database")]
		public static void CreateBaseUnitDatabase ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<BaseUnitDatabase> (_path);
			}
		}

		//Upgrade
		[MenuItem("Assets/BSS/Create/Upgradable/UpAttack")]
		public static void CreateUpAttack ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<UpAttack> (_path);
			}
		}
	}
}

