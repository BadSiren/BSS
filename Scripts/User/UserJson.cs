using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;
using BSS;
using Sirenix.OdinInspector;


namespace BSS {
	public class UserJson : SerializedMonoBehaviour
	{
		private static UserJson _instance;
		public static UserJson instance {
			get {
				return _instance;
			}
		}

		public string moneyName="Money";
		public string gemName="Gem";
		public string inventoryName="Inventory";

		public Dictionary<string,int> containerMax=new Dictionary<string,int>();
		[Header("Don't Touch")]
		public Dictionary<string,List<LobbyItem>> containers = new Dictionary<string,List<LobbyItem>> ();


		void Awake() {
			if (_instance == null) {
				_instance = this;
			} else {
				Destroy (gameObject);
				return;
			}
			DontDestroyOnLoad (gameObject);


			intialize ();
		}

		void intialize() {
			if (!ES2.Exists (moneyName)) {
				ES2.Save (500, moneyName);
			}

			if (!ES2.Exists (gemName)) {
				ES2.Save (0, gemName);
			}
			foreach (var it in containerMax) {
				containers [it.Key] = new List<LobbyItem> ();
				containers [it.Key].Capacity = it.Value;
				ES2.Delete (it.Key);
				if (!ES2.Exists (it.Key)) {
					ES2.Save (new List<string>(), it.Key);
				}
				itemInitialize (it.Key);
			}
		}

		//Monery Function
		public void addMoney(int _money) {
			int loadMoney=ES2.Load<int> (moneyName);
			ES2.Save<int> (loadMoney + _money, moneyName);
			BaseEventListener.onPublishInt ("Money", loadMoney + _money);
		}
		public bool useMoney(int _money) {
			int loadMoney=ES2.Load<int> (moneyName);
			if (loadMoney < _money) {
				return false;
			}
			ES2.Save<int> (loadMoney - _money, moneyName);
			BaseEventListener.onPublishInt ("Money", loadMoney - _money);
			return true;
		}
		public bool isMoney(int _money) {
			int loadMoney=ES2.Load<int> (moneyName);
			if (loadMoney < _money) {
				return false;
			}
			return true;
		}
		public void addGem(int _gem) {
			int loadGem=ES2.Load<int> (gemName);
			ES2.Save<int> (loadGem + _gem, gemName);
			BaseEventListener.onPublishInt ("Gem", loadGem + _gem);
		}
		public bool useGem(int _gem) {
			int loadGem=ES2.Load<int> (gemName);
			if (loadGem < _gem) {
				return false;
			}
			ES2.Save<int> (loadGem - _gem, gemName);
			BaseEventListener.onPublishInt ("Gem", loadGem - _gem);
			return true;
		}
		public bool isGem(int _gem) {
			int loadGem=ES2.Load<int> (gemName);
			if (loadGem < _gem) {
				return false;
			}
			return true;
		}

		//Container Function
		public bool addItem(LobbyItem item,string container,bool isSave=true) {
			if (getEmptySpace (container) == 0) {
				Debug.Log ("Add fail");
				return false;
			}
			containers [container].Add (item);

			//ES2 Save
			if (isSave) {
				var _container = ES2.LoadList<string> (container);
				UserItem userItem = new UserItem (item);
				_container.Add (userItem.toJson ());
				ES2.Save (_container, container);
			}
			BaseEventListener.onPublish (container+"Update");
			return true;
		}
		public bool removeItem(int slot,string container) {
			if (slot>containers[container].Count-1) {
				Debug.Log ("Remove fail");
				return false;
			}
			containers[container].RemoveAt (slot);

			//ES2 Save
			var _container=ES2.LoadList<string> (container);
			_container.RemoveAt (slot);
			ES2.Save (_container, container);
			BaseEventListener.onPublish (container+"Update");
			return true;
		}
		public bool changeSlot(int preSlot,int nowSlot,string container) {
			if (preSlot == nowSlot ||preSlot>containers[container].Count-1 || nowSlot>containers[container].Count-1 ) {
				Debug.Log ("Change fail");
				return false;
			}
			LobbyItem temp = containers[container][preSlot];
			containers[container] [preSlot] = containers[container][nowSlot];
			containers[container] [nowSlot] = temp;

			//ES2 Save
			var _container=ES2.LoadList<string> (container);
			string _temp = _container [preSlot];
			_container [preSlot] = _container [nowSlot];
			_container [nowSlot] = _temp;
			ES2.Save (_container, container);
			BaseEventListener.onPublish (container+"Update");

			return true;
		}
		public bool changeContainer(int itemSlot,string nowContainer,string nextContainer) {
			if (itemSlot>containers[nowContainer].Count-1) {
				Debug.Log ("transport fail");
				return false;
			}
			if (!addItem (containers [nowContainer] [itemSlot], nextContainer)) {
				return false;
			}
			removeItem (itemSlot, nowContainer);
			return true;
		}
		public int getEmptySpace(string container) {
			if (containerMax.ContainsKey(container)) {
				return containerMax [container]-containers[container].Count;
			}
			Debug.LogError ("No Container!");
			return 0;
		}
		public LobbyItem getLobbyItem(int slot,string container) {
			if (slot>containers[container].Count-1) {
				Debug.LogError ("No Item");
				return null;
			}
			return containers [container] [slot];
		}
		public bool existsSlot(int slot,string container) {
			if (slot>containers[container].Count-1) {
				return false;
			}
			return true;
		}

		private void itemInitialize(string container) {
			List<string> jsonList=ES2.LoadList<string> (container);
			foreach (var json in jsonList) {
				var userItem=new UserItem (json);
				addItem(userItem.toItem (),container,false);
			}
		}
	}
}

