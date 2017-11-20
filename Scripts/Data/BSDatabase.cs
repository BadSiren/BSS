using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.LobbyItemSystem;
using BSS.Unit;

namespace BSS {
	public class BSDatabase : MonoBehaviour
	{
		public static BSDatabase instance;
		public LobbyItemDatabase lobbyItemDatabase;
		public ActivableDatabase activableDatabase;

		void Awake() {
			if (instance == null) {
				instance = this;
			} else {
				Destroy (gameObject);
				return;
			}
			DontDestroyOnLoad (gameObject);
		}
	}
}

