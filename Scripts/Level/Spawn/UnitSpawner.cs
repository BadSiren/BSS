using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;
using Sirenix.OdinInspector;

namespace BSS.Level {
	public class UnitSpawner : SerializedMonoBehaviour
	{
		public bool isEnemy = true;
		public float addHeathMultiple=0f;
		public float addDamageMultiple=0f;
		[System.Serializable]
		public class SpawnUnit
		{
			public string uIndex;
			public int number=1;
		}
		public class SpawnData {
			public List<SpawnUnit> spawnUnits =new List<SpawnUnit>();
			[TextArea()]
			public string spawnText;

			public List<GameObject> getSpawnUnits() {
				var units = new List<GameObject> ();
				var database = BSDatabase.instance.baseUnitDatabase.unitPrefabs;
				foreach (var it in spawnUnits) {
					if (database.ContainsKey (it.uIndex)) {
						for (int i = 0; i < it.number; i++) {
							var obj = GameObject.Instantiate (database [it.uIndex]);
							units.Add (obj);
						}
					}
				}
				return units;
			}
		}

		public List<SpawnData> spawnDatas = new List<SpawnData> ();
		public int maxLevel {
			get {
				return spawnDatas.Count;
			}
		}


		void Awake() {
			if (maxLevel == 0) {
				Destroy (gameObject);
				return;
			}
		}

		public List<GameObject> spawnOnce(int level) {
			if (maxLevel - 1 < level) {
				return null;
			}

			List<GameObject> units = spawnDatas [level].getSpawnUnits ();
			foreach (var it in units) {
				it.transform.localPosition = transform.localPosition+new Vector3(Random.Range(-1f,1f),Random.Range(-3f,3f),0f);
				BaseUnit unit=it.GetComponent<BaseUnit> ();
				unit.maxHealth = unit.maxHealth * (1f +addHeathMultiple);
				unit.health = unit.maxHealth;
				Attackable attackable=it.GetComponent<Attackable> ();
				if (attackable != null) {
					attackable.changeDamage += attackable.initDamage * addDamageMultiple;
				}
				if (isEnemy) {
					unit.setEnemy ();
				}
			}
			return units;
		}
		public string getSpawnText(int level) {
			if (maxLevel - 1 < level) {
				return "";
			}
			return spawnDatas [level].spawnText;
		}
	}
}
