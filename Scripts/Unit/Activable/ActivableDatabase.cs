using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public class ActivableDatabase : ScriptableObject
	{
		public static ActivableDatabase instance;
		public List<Activable> activableList=new List<Activable>();

		public Activable getActivable(string _ID){
			Activable activable=activableList.Find(x=>x.ID==_ID);
			return activable;
		}
		/*
		void initialize() {
			foreach (var it in activablePrefabDatabaseList) {
				var act=it.GetComponent<Activable> ();
				activableDatabaseList.Add (act);
				activablePrefabDatabaseDic [act.actIndex] = it;
				activableDatabaseDic [act.actIndex] = act;
			}
		}
		*/
	}

}

