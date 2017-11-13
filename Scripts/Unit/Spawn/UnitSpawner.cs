using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public class UnitSpawner : MonoBehaviour
	{
		public List<SpawnData> spawnDatas;
		public Vector3 destination;

		void Awake() {
			if (spawnDatas.Count == 0) {
				Destroy (gameObject);
				return;
			}
		}

		public List<GameObject> spawnOnce(int level) {
			if (spawnDatas.Count - 1 < level) {
				return null;
			}
			List<GameObject> units = spawnDatas [level].getUnits ();
			foreach (var it in units) {
				it.transform.localPosition = transform.localPosition+new Vector3(Random.Range(-1f,1f),Random.Range(-3f,3f),0f);
				if (destination != Vector3.zero) {
					it.SendMessage ("toPatrol", destination, SendMessageOptions.DontRequireReceiver);
				}
			}
			return units;
		}
		public string getSpawnText(int level) {
			if (spawnDatas.Count - 1 < level) {
				return "";
			}
			return spawnDatas [level].text;
		}
	}
}
