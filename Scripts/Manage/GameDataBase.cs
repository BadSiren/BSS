using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventsPlus;
using BSS.Unit;
using Sirenix.OdinInspector;

namespace BSS.Play {
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
		public Dictionary<string,int> upgrades=new Dictionary<string,int>();
		public Dictionary<string,List<Upgradable>> upListeners=new Dictionary<string,List<Upgradable>>();


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
		public int getUpgradeLevel(string upID) {
			if (!upgrades.ContainsKey (upID)) {
				upgrades [upID] = 0;
			}
			return upgrades [upID];
		}
		public void setUpgradeLevel(string upID,int _level) {
			upgrades [upID] = _level;
			if (upListeners.ContainsKey(upID)) {
				foreach (var it in upListeners[upID]) {
					it.onUpgradeApply ();
				}
			}
		}
		public void addUpListener(Upgradable _up) {
			if (!upListeners.ContainsKey (_up.ID)) {
				upListeners [_up.ID] = new List<Upgradable> ();
			}
			if (!upListeners [_up.ID].Contains (_up)) {
				upListeners [_up.ID].Add (_up);
			}
		}
		public void removeUpListener(Upgradable _up) {
			if (upListeners.ContainsKey (_up.ID)) {
				if (upListeners [_up.ID].Contains(_up)) {
					upListeners [_up.ID].Remove (_up);
				}
			}
		}

	}
}
