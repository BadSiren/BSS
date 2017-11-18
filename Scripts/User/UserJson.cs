using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;

public class UserJson : MonoBehaviour
{
	[System.Serializable]
	public class UserItem
	{
		public string ID;

		public UserItem(string _id) {
			ID=_id;
		}
	}

	private static UserJson _instance;
	public static UserJson instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<UserJson> ();
			}
			if (_instance == null) {
				Debug.LogError ("No UserJson");
			}
			return _instance;
		}
	}

	public string moneyName="Money";
	public string gemName="Gem";

	public string inventoryName="Inventory";
	public int inventoryMax=60;
	public List<string> equipContainers = new List<string> ();
	public List<int> equipContainerMax = new List<int> ();
	public Dictionary<string,int> containerMax=new Dictionary<string,int>();


	void Awake() {
		intialize ();
	}
		
	void intialize() {
		containerMaxInit ();
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

	}
	private void containerMaxInit() {
		containerMax [inventoryName] = inventoryMax;
		if (equipContainers.Count != equipContainerMax.Count) {
			Debug.LogError ("Container Max Error");
		}
		for (int i=0;i<equipContainers.Count;i++) {
			containerMax [equipContainers [i]] = equipContainerMax [i];
		}
	}

	//Inventory Function
	public bool addItem(UserItem userItem,string _container) {
		if (!ES2.Exists (_container) || !existLobbyItem(userItem)) {
			Debug.Log ("Item not found");
			return false;
		}
		List<string> container=ES2.LoadList<string> (_container);
		if (container.Count >= containerMax[_container]) {
			Debug.Log ("Inventory Full");
			return false;
		}
		string json=JsonUtility.ToJson (userItem);
		container.Add (json);
		ES2.Save (container, _container);
		return true;
	}
	public bool removeItem(int slot,string _container) {
		if (!ES2.Exists (_container)) {
			return false;
		}
		List<string> container=ES2.LoadList<string> (_container);
		if (slot>container.Count-1) {
			Debug.Log ("No Item");
			return false;
		}
		container.RemoveAt (slot);
		ES2.Save (container, _container);
		return true;
	}
	public bool changeItem(int preSlot,int nowSlot,string _container) {
		if (preSlot == nowSlot || !ES2.Exists (_container)) {
			return false;
		}
		List<string> container=ES2.LoadList<string> (_container);
		if (preSlot>container.Count-1 || nowSlot>container.Count-1 ) {
			Debug.Log ("No Item");
			return false;
		}
		string temp = container [preSlot];
		container [preSlot] = container [nowSlot];
		container [nowSlot] = temp;
		ES2.Save (container, _container);
		return true;
	}
	public bool transportItem(int _itemSlot,string _nowContainer,string _nextContainer) {
		if (!ES2.Exists (_nowContainer) || !ES2.Exists (_nextContainer)) {
			return false;
		}
		List<string> nowContainer=ES2.LoadList<string> (_nowContainer);
		if (_itemSlot>nowContainer.Count-1) {
			Debug.Log ("No Item");
			return false;
		}
		UserItem userItem=getUserItem (_itemSlot, _nowContainer);
		if (userItem == null || !addItem (userItem, _nextContainer)) {
			return false;
		}
		removeItem (_itemSlot, _nowContainer);
		return true;
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
		return LobbyItemDatabase.instance.getItem (userItem.ID);
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
	public bool existLobbyItem(UserItem userItem) {
		if (userItem == null || LobbyItemDatabase.instance.getItem (userItem.ID) == null) {
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

