using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	
	public class SpawnData : MonoBehaviour
	{
		[System.Serializable]
		public class UnitPrefabData
		{
			public GameObject unitPrefab;
			public int number=1;
			public float healthMultiple=1f;
			public float damageMultiple=1f;
		}
		public bool isEnemy = true;
		public List<UnitPrefabData> unitDatas;
		[TextArea()]
		public string text;

		private List<GameObject> spawnUnits=new List<GameObject>();

		public List<GameObject> getUnits() {
			spawnUnits.Clear ();
			foreach (var it in unitDatas) {
				for (int i = 0; i < it.number; i++) {
					var obj = GameObject.Instantiate (it.unitPrefab, transform.localPosition, Quaternion.identity);
					var unit=obj.GetComponent<BaseUnit> ();
					var attackable=obj.GetComponent<Attackable> ();
					if (unit != null) {
						unit.maxHealth = unit.maxHealth * it.healthMultiple;
						unit.health = unit.maxHealth;
						if (isEnemy) {
							unit.setEnemy ();
						}
					}
					if (attackable != null) {
						attackable.changeDamage += attackable.initDamage*(it.damageMultiple-1f);
					}
					spawnUnits.Add (obj);
				}
			}
			return spawnUnits;
		}
	}
}

