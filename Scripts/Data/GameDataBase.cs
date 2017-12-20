using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;
using Sirenix.OdinInspector;

namespace BSS{
	public class GameDataBase : SerializedMonoBehaviour
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
		public int maxPopulation {
			get {
				return getUpgradeLevel ("MaxPopulation");
			}
		}

		public Dictionary<string,int> upgrades=new Dictionary<string,int>();


		void Awake() {
			instance = this;
			money = 0;
			food = 0;
		}

		public bool useMoneyFood(int useMoney,int useFood) {
			if (money < useMoney || food<useFood) {
				return false;
			}
			money -= useMoney;
			food -= useFood;
			return true;
		}
		public bool isUpgrade(string upID,int needLevel) {
			if (needLevel > getUpgradeLevel (upID)) {
				return false;
			}
			return true;
		}
		public int getUpgradeLevel(string upID) {
			if (!upgrades.ContainsKey (upID)) {
				upgrades [upID] = 0;
			}
			return upgrades [upID];
		}
		public void setUpgradeLevel(string upID,int _level) {
			upgrades [upID] = _level;
			BaseEventListener.onPublishInt("Up"+upID,_level);
		}
		public bool isPopulation(int needPopulation) {
			if (BaseUnit.totalPopulation + needPopulation > maxPopulation) {
				return false;
			}
			return true;
		}

	}
}
