using UnityEngine;
using System.Collections;
using BSS.Play;

namespace BSS.Unit {
	public class Rewardable : MonoBehaviour
	{
		public int money;
		public int food;


		void onDieEvent() {
			GameDataBase.instance.money += money;
			GameDataBase.instance.food += food;
		}
	}
}

