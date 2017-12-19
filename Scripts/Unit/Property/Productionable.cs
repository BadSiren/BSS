using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public class Productionable : SerializedMonoBehaviour
	{
		public enum EProductType {
			UnitBuy,Upgrade
		}
		public EProductType eProductType;
		public List<Dictionary<string,string>> productions=new List<Dictionary<string,string>>();

		private BaseUnit owner;

		void Awake() {
			owner = GetComponent<BaseUnit> ();
		}
		void Start() {
			foreach (var it in productions) {
				addActivable (eProductType.ToString(),it);
			}
		}

		private void addActivable(string ID,Dictionary<string,string> product) {
			//var activable=BSDatabase.instance.activableDatabase.createActivable<Activable> (ID, product);
			//owner.addActivable (activable);
		}
	}
}

