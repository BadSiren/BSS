using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.UI;

namespace BSS.Unit {
	public class Activables : SerializedMonoBehaviour
	{
		public string initPage="Base";
		public Dictionary<string,List<Activable>> activableList;
		private const int MAX_COUNT=9;

		private BaseUnit owner;

		void Awake() {
			owner = GetComponentInParent<BaseUnit> ();
			var acts=new List<Activable>(GetComponentsInChildren<Activable> ());
			foreach (var it in acts) {
				registActivable (it);
			}
		}
	
		public Activable getActivableOrNull(string category,int index) {
			if (!activableList.ContainsKey (category) || activableList[category].Count-1<index) {
				return null;
			}
			return activableList [category] [index];
		}
		public void registActivable(Activable act) {
			if (!activableList.ContainsKey (act.category)) {
				activableList [act.category] = new List<Activable> ();
				for (int i = 0; i < MAX_COUNT; i++) {
					activableList[act.category].Add(null);
				}
			}
			activableList [act.category][act.index]= act;
		}
		public void unregistActivable(string category,int index) {
			if (!activableList.ContainsKey (category)) {
				return;
			}
			GameObject.Destroy (activableList [category] [index]);
		}
		public GameObject getContainerOrCreate(string category){
			Transform containerTr = transform.Find (category);
			if (containerTr == null) {
				var temp = new GameObject (category);
				temp.transform.SetParent (containerTr);
				containerTr = temp.transform;
			}
			return containerTr.gameObject;
		}
			
	}
}

