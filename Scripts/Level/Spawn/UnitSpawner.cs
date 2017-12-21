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

		public DicSelector dicSelector;
			
		public void spawnOnce(int level) {
			var levelInfo = dicSelector.getDic (level);
			var spawnKeys=DictionaryUtil.getKeyListInStartWith (levelInfo, "SpawnUnit");
			List<GameObject> spawnUnits = new List<GameObject> ();
			foreach (var key in spawnKeys) {
				string unitID = levelInfo [key].Split ('/') [0];
				int count = int.Parse (levelInfo [key].Split ('/') [1]);
				GameObject unitPrefab = BSDatabase.instance.baseUnitDatabase.unitPrefabs [unitID];

				for (int i = 0; i < count; i++) {
					GameObject copyObj = UnitUtils.CreateUnit (unitPrefab, transform.localPosition + new Vector3 (Random.Range (-1f, 1f), Random.Range (-3f, 3f), 0f), UnitTeam.Blue);
					BaseEventListener.onPublishGameObject ("SpawnUnit", copyObj);
					spawnUnits.Add (copyObj);
					BaseUnit unit = copyObj.GetComponent<BaseUnit> ();
					unit.maxHealth = unit.maxHealth * (1f + addHeathMultiple);
					unit.health = unit.maxHealth;
					Attackable attackable = copyObj.GetComponent<Attackable> ();
					if (attackable != null) {
						attackable.changeDamage += attackable.initDamage * addDamageMultiple;
					}
				}
			}
			StartCoroutine (checkDestroyUnits (spawnUnits));
		}
		public string getSpawnText(int level) {
			var levelInfo = dicSelector.getDic (level);
			return levelInfo ["Text"];
		}

		IEnumerator checkDestroyUnits(List<GameObject> units) {
			while (true) {
				if (units.Count == units.FindAll (x => x == null).Count) {
					BaseEventListener.onPublish ("NothingSpawnUnit");
					break;
				}
				yield return new WaitForSeconds (2f);
			}
		}
	}
}
