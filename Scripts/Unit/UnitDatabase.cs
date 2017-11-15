using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public class UnitDatabase : MonoBehaviour
	{
		private static UnitDatabase _instance;
		public static UnitDatabase instance {
			get {
				if (_instance == null) {
					_instance = GameObject.FindObjectOfType<UnitDatabase> ();
				}
				if (_instance == null) {
					Debug.LogError ("No Unit Database");
				}
				return _instance;
			}
		}
		public List<GameObject> unitPrefabDatabaseList;
		[HideInInspector]
		public List<BaseUnit> unitDatabaseList = new List<BaseUnit> ();
		public Dictionary<string,GameObject> unitPrefabDatabaseDic=new Dictionary<string,GameObject>();
		public Dictionary<string,BaseUnit> unitDatabaseDic=new Dictionary<string,BaseUnit>();


		void Awake() {
			if (_instance != null) {
				Destroy (gameObject);
				return;
			}

			DontDestroyOnLoad(gameObject);
			initialize ();
		}

		void initialize() {
			foreach (var it in unitPrefabDatabaseList) {
				var unit=it.GetComponent<BaseUnit> ();
				unitDatabaseList.Add (unit);
				unitPrefabDatabaseDic [unit.uIndex] = it;
				unitDatabaseDic [unit.uIndex] = unit;
			}
		}
	}

}