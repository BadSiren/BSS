using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BSS.Unit {
	public class ActivableEditor : MonoBehaviour
	{
		//Activable
		[MenuItem("Assets/BSS/Create/Acitvable/Database")]
		public static void CreateActivableDatabase ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<ActivableDatabase> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/Acitvable/Destroy")]
		public static void CreateDestroy ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<ActDestroy> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/Acitvable/UnitBuy")]
		public static void CreateUnitBuy ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<ActUnitBuy> (_path);
			}
		}
		[MenuItem("Assets/BSS/Create/Acitvable/Upgrade")]
		public static void CreateUpgrade ()
		{
			string _path = EditorUtility.SaveFilePanel ("Create Asset", "Assets/BSS/", "", "asset");
			if( _path.Length != 0 )
			{
				ScriptableObjectUtility.CreateAsset<ActUpgrade> (_path);
			}
		}
	}
}

