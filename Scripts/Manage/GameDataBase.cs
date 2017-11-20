using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventsPlus;
using BSS.Unit;

namespace BSS.Play {
	public class GameDataBase : MonoBehaviour
	{
		public static GameDataBase instance;

		private int _money;
		public int money {
			get {
				return _money;
			}
			set { 
				_money = value;
				BaseEventListener.onPublishInt ("GameMoney", _money);
			}
		}
		private int _food;
		public int food {
			get {
				return _food;
			}
			set { 
				_food = value;
				BaseEventListener.onPublishInt ("Food", _food);
			}
		}
		public Dictionary<string,int> upgrades=new Dictionary<string,int>();


		void Awake()
		{
			instance = this;
		}

		public bool useMoneyFood(int useMoney,int useFood) {
			if (money < useMoney || food<useFood) {
				return false;
			}
			money -= useMoney;
			food -= useFood;
			return true;
		}
		public int getUpgradeLevel(string upIndex) {
			if (!upgrades.ContainsKey (upIndex)) {
				upgrades [upIndex] = 0;
			}
			return upgrades [upIndex];
		}
		public void setUpgradeLevel(string upIndex,int _level) {
			upgrades [upIndex] = _level;
		}
	}
}
