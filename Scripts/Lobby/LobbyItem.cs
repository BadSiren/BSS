using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Lobby {
	public class LobbyItem : MonoBehaviour
	{
		public string iIndex;
		public string iName;
		public string content;
		public Sprite icon;
		public Dictionary<string,int> properties=new Dictionary<string,int>();


	}
}

