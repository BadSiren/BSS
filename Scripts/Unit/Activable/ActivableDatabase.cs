using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public class ActivableDatabase : MonoBehaviour
	{
		private static ActivableDatabase _instance;
		public static ActivableDatabase instance {
			get {
				if (_instance == null) {
					_instance = GameObject.FindObjectOfType<ActivableDatabase> ();
				}
				if (_instance == null) {
					Debug.LogError ("No Unit Database");
				}
				return _instance;
			}
		}
		public List<GameObject> activablePrefabDatabaseList;
		[HideInInspector]
		public List<Activable> activableDatabaseList = new List<Activable> ();
		public Dictionary<string,GameObject> activablePrefabDatabaseDic=new Dictionary<string,GameObject>();
		public Dictionary<string,Activable> activableDatabaseDic=new Dictionary<string,Activable>();


		void Awake() {
			if (_instance != null) {
				Destroy (gameObject);
				return;
			}

			DontDestroyOnLoad(gameObject);
			initialize ();
		}

		void initialize() {
			foreach (var it in activablePrefabDatabaseList) {
				var act=it.GetComponent<Activable> ();
				activableDatabaseList.Add (act);
				activablePrefabDatabaseDic [act.actIndex] = it;
				activableDatabaseDic [act.actIndex] = act;
			}
		}
	}

}

