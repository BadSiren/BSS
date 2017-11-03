using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public class UnitSpawner : MonoBehaviour
	{
		public List<SpawnData> spawners;
		public Vector3 spawnPosition;
		public UnitTeam spawnTeam;
		public bool isDeleteRigidbody=true;

		public bool isOperated;


		private int _level;
		public int level {
			get {
				return _level;
			}
			set { 
				_level = value;
				pLevel.publish (value);
			}
		}
		public PublisherInt pLevel;

		private int maxLevel;
		private SpawnData nowSpawn;
		private int spawnCount;

		void Awake() {
			pLevel.initialize ();

			maxLevel = spawners.Count;
			if (isOperated) {
				startLevel ();
			}
		}
		void OnDestroy() {
			pLevel.clear ();
		}


		IEnumerator stagePlay() {
			bool loop = true;
			while (loop) {
				yield return new WaitForSeconds (nowSpawn.interval);
				if (isOperated) { 
					var obj=GameObject.Instantiate (nowSpawn.unitPrefab, spawnPosition, Quaternion.identity);
					if (nowSpawn.isReward) {
						var rewardable=obj.AddComponent<Rewardable> ();
						rewardable.money = nowSpawn.rewardMoney;
						rewardable.food = nowSpawn.rewardFood;
					}
					var unit=obj.GetComponent<BaseUnit> ();
					unit.team = spawnTeam;
					if (isDeleteRigidbody) {
						Destroy(unit.GetComponent<Rigidbody2D> ());
					}
					spawnCount += 1;
					if (spawnCount >= nowSpawn.count) {
						spawnCount = 0;
						loop = false;
						nextLevel ();
					}
				}
			}
		}
		public void startLevel() {
			level = 1;
			nowSpawn = spawners [level-1];
			StartCoroutine (stagePlay ());
		}
		private void nextLevel() {
			if (maxLevel == level) {
				clearLevel ();
				return;
			}
			level += 1;
			nowSpawn = spawners [level-1];
			StartCoroutine (stagePlay ());
		}
		public void clearLevel() {
			
		}

	}
}
