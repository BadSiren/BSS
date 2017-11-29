﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Play;

namespace BSS.LobbyItemSystem {
	public class LobbyItemDatabase : SerializedScriptableObject
	{
		public struct RairityInfo {
			public string title;
			public Color col;
		}

		public List<LobbyItem> lobbyItems = new List<LobbyItem> ();
		public Dictionary<int,RairityInfo> rairityInfos=new Dictionary<int,RairityInfo>();
		public Dictionary<string,EquipProperty> equipProperties=new Dictionary<string,EquipProperty>();

		public List<T> getLobbyItems<T>() where T : LobbyItem{
			List<T> items = new List<T> ();
			foreach (var it in lobbyItems.FindAll (x => x is T)) {
				items.Add (it as T);
			}
			return items;
		}
		public T getLobbyItem<T>(string _ID) where T : LobbyItem{
			List<T> items = getLobbyItems<T> ();
			return items.Find (x => x.ID == _ID);
		}

		public T createLobbyItem<T>(string _ID) where T : LobbyItem   {
			var temp=lobbyItems.Find (x => x.ID == _ID);
			T item = (ScriptableObject.Instantiate (temp) as T);
			if (item is LobbyEquipItem) {
				(item as LobbyEquipItem).propertiesRand ();
			}
			return item;
		}

		public T getEquipProperty<T>(string _ID) where T : EquipProperty{
			if (!equipProperties.ContainsKey (_ID)) {
				Debug.LogError ("No Property");
			}
			return (equipProperties [_ID] as T);
		}
		public T createEquipProperty<T>(string _ID,Dictionary<string,string> args) where T : EquipProperty{
			if (!equipProperties.ContainsKey (_ID)) {
				Debug.LogError ("No Property");
			}
			var property=ScriptableObject.Instantiate (equipProperties [_ID]) as T;
			property.initialize (args);
			return property;
		}

	}
}
