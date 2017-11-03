using UnityEngine;
using System.Collections;
using EventsPlus;

namespace BSS.Data {
	public class GameDataBase : MonoBehaviour
	{
		public static GameDataBase instance;
		public int initMoney;
		public int initFood;
		public int initShortAttackUp;
		public int initLongAttackUp;
		public int initArmorUp;

		public PublisherInt pMoney;
		public PublisherInt pFood;

		public PublisherInt pShortAttackUp;
		public PublisherInt pLongAttackUp;
		public PublisherInt pArmorUp;

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

		private int _shortAttackUp;
		public int shortAttackUp {
			get {
				return _shortAttackUp;
			}
			set { 
				_shortAttackUp = value;
				pShortAttackUp.publish (value);
			}
		}
		private int _longAttackUp;
		public int longAttackUp {
			get {
				return _longAttackUp;
			}
			set { 
				_longAttackUp = value;
				pLongAttackUp.publish (value);
			}
		}
		private int _armorUp;
		public int armorUp {
			get {
				return _armorUp;
			}
			set { 
				_armorUp = value;
				pArmorUp.publish (value);
			}
		}



		void Awake()
		{
			instance = this;

			pMoney.initialize();
			pFood.initialize();
			pShortAttackUp.initialize();
			pLongAttackUp.initialize();
			pArmorUp.initialize();

		}
		void Start() {
			money = initMoney;
			food = initFood;
			shortAttackUp = initShortAttackUp;
			longAttackUp = initLongAttackUp;
			armorUp = initArmorUp;
		}

		public void OnDestroy()
		{
			pMoney.clear();
			pFood.clear ();
			pShortAttackUp.clear();
			pLongAttackUp.clear();
			pArmorUp.clear();

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
		private void getShortAttackUpEvent(MessageCallback callback) {
			callback.callback = shortAttackUp;
		}
		private void increaseShortAttackUpEvent() {
			shortAttackUp++;
		}

		private void getLongAttackUpEvent(MessageCallback callback) {
			callback.callback = longAttackUp;
		}
		private void increaseLongAttackUpEvent() {
			longAttackUp++;
		}

		private void getArmorUpEvent(MessageCallback callback) {
			callback.callback = armorUp;
		}
		private void increaseArmorUpEvent() {
			armorUp++;
		}
	}
}
