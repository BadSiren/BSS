using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventsPlus;
using BSS.Unit;

namespace BSS.Data {
	public class GameDataBase : MonoBehaviour
	{
		public static GameDataBase instance;
		public int initMoney;
		public int initFood;

		public Dictionary<string,int> upgrades = new Dictionary<string,int> ();

		public PublisherInt pMoney;
		public PublisherInt pFood;

		private int _money;
		public int money {
			get {
				return _money;
			}
			set { 
				_money = value;
				pMoney.publish (value);
			}
		}
		private int _food;
		public int food {
			get {
				return _food;
			}
			set { 
				_food = value;
				pFood.publish (value);
			}
		}



		void Awake()
		{
			instance = this;

			pMoney.initialize();
			pFood.initialize();

		}
		void Start() {
			money = initMoney;
			food = initFood;
		}

		public void OnDestroy()
		{
			pMoney.clear();
			pFood.clear ();
		}

		public bool useMoneyFood(int useMoney,int useFood) {
			if (money < useMoney || food<useFood) {
				return false;
			}
			money -= useMoney;
			food -= useFood;
			return true;
		}

		//ActEvent
		public void increaseUpgrade(string upIndex) {
			if (upgrades.ContainsKey(upIndex)) {
				upgrades [upIndex] +=1;
			} else {
				upgrades [upIndex] = 1;
			}
			UpBase.allApplyUpgrade (upIndex);
		}
		public int getUpgrade(string upIndex) {
			if (upgrades.ContainsKey(upIndex)) {
				return upgrades [upIndex];
			} 
			return 0;
		}
	}
}
