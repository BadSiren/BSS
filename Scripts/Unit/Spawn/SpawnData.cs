using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class SpawnData : MonoBehaviour
	{
		public GameObject unitPrefab;
		public int count;
		public float interval;
		public bool isReward=true;
		public int rewardMoney;
		public int rewardFood;
	}
}

