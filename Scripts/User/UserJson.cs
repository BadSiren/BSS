using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;
using BSS;
using Sirenix.OdinInspector;

public class UserJson : SerializedMonoBehaviour
{
	[System.Serializable]
	public class UserItem
	{
		public string ID;
		public int index;

		public UserItem(string _id) {
			ID=_id;
			index=0;
		}
	}

	private static UserJson _instance;
	public static UserJson instance {
		get {
			return _instance;
		}
	}

	public string moneyName="Money";
	public string gemName="Gem";

	public string inventoryName="Inventory";
	public List<string> equipContainers = new List<string> ();
	public Dictionary<string,int> containerMax=new Dictionary<string,int>();


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
			ES2.Save (0, moneyName);
		}

		if (!ES2.Exists (gemName)) {
			ES2.Save (0, gemName);
		}

		if (!ES2.Exists (inventoryName)) {
			ES2.Save (new List<string>(), inventoryName);
		}
		for (int i = 0; i < equipContainers.Count; i++) {
			if (!ES2.Exists (equipContainers[i])) {
				ES2.Save (new List<string>(), equipContainers[i]);
			}
		}
		addMoney (10000);
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

	//Container Function
	public bool addItem(UserItem userItem,string _container) {
		List<string> container=ES2.LoadList<string> (_container);
		var _item=BSDatabase.instance.lobbyItemDatabase.lobbyItems.Find (x => x.ID == userItem.ID);
		if (_item==null ||  getEmptySpace (_container) == 0) {
			Debug.Log ("add fail");
			return false;
		}

		string json=JsonUtility.ToJson (userItem);
		container.Add (json);
		ES2.Save (container, _container);
		return true;
	}
	public bool removeItem(int slot,string _container) {
		List<string> container=ES2.LoadList<string> (_container);
		if (slot>container.Count-1) {
			Debug.Log ("remove fail");
			return false;
		}
		container.RemoveAt (slot);
		ES2.Save (container, _container);
		return true;
	}
	public bool changeItem(int preSlot,int nowSlot,string _container) {
		List<string> container=ES2.LoadList<string> (_container);
		if (preSlot == nowSlot ||preSlot>container.Count-1 || nowSlot>container.Count-1 ) {
			Debug.Log ("change fail");
			return false;
		}
		string temp = container [preSlot];
		container [preSlot] = container [nowSlot];
		container [nowSlot] = temp;
		ES2.Save (container, _container);
		return true;
	}
	public bool transportItem(int _itemSlot,string _nowContainer,string _nextContainer) {
		List<string> nowContainer=ES2.LoadList<string> (_nowContainer);
		if (_itemSlot>nowContainer.Count-1) {
			Debug.Log ("transport fail");
			return false;
		}
		UserItem userItem=getUserItem (_itemSlot, _nowContainer);
		if (userItem == null || !addItem (userItem, _nextContainer)) {
			return false;
		}
		removeItem (_itemSlot, _nowContainer);
		return true;
	}
	public int getEmptySpace(string _container) {
		if (containerMax.ContainsKey(_container)) {
			return containerMax [_container]-getCount(_container);
		}
		Debug.Log ("No Container!");
		return 0;
	}
	private void resetItems(string _container) {
		ES2.Delete (_container);
	}
	private void resetItems(List<string> _containers) {
		foreach (var it in _containers) {
			ES2.Delete (it);
		}
	}

	//Item Function
	public UserItem getUserItem(int slot,string _container) {
		if (!ES2.Exists (_container)) {
			return null;
		}
		List<string> container=ES2.LoadList<string> (_container);
		if (slot > container.Count - 1) {
			Debug.Log ("No Item");
			return null;
		}
		UserItem userItem=JsonUtility.FromJson<UserItem> (container [slot]);
		return userItem;
	}
	public LobbyItem getLobbyItem(int slot,string _container) {
		UserItem userItem = getUserItem (slot,_container);
		if (userItem == null) {
			return null;
		}

		return BSDatabase.instance.lobbyItemDatabase.lobbyItems.Find (x => x.ID == userItem.ID);
	}
	public List<LobbyItem> getLobbyItems(string _container) {
		List<LobbyItem> lobbyItems = new List<LobbyItem> ();
		for (int i = 0; i < getCount (_container); i++) {
			var _item=getLobbyItem (i, _container);
			if (_item!=null) {
				lobbyItems.Add (_item);
			}
		}
		return lobbyItems;
	}
	public List<LobbyEquipItem> getLobbyEquipItems(string _container) {
		List<LobbyItem> lobbyItems = getLobbyItems (_container);
		lobbyItems=lobbyItems.FindAll (x => x is LobbyEquipItem);
		List<LobbyEquipItem> lobbyEquipItems = new List<LobbyEquipItem> ();
		for (int i = 0; i < lobbyItems.Count; i++) {
			lobbyEquipItems.Add (lobbyItems [i] as LobbyEquipItem);
		}
		return lobbyEquipItems;
	}
	public bool existLobbyItem(UserItem userItem) {
		if (userItem == null || BSDatabase.instance.lobbyItemDatabase.lobbyItems.Find (x => x.ID == userItem.ID) == null) {
			return false;
		}
		return true;
	}
	public int getCount(string _container) {
		if (!ES2.Exists (_container)) {
			return 0;
		}
		List<string> container=ES2.LoadList<string> (_container);
		return container.Count;
	}



}

