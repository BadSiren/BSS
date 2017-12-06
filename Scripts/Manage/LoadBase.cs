using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using BSS.Level;
using BSS.LobbyItemSystem;
using Sirenix.OdinInspector;

namespace BSS {
	public class LoadBase : MonoBehaviour
	{
		public static LoadBase instance;

		public string lobbyScene="LobbyScene";
		public string playScene="PlayScene";
		public string itemScene="ItemScene";

		public int selcectMode;
		public int selcectLevel;

		void Awake() {
			if (instance == null) {
				instance = this;
			} else {
				Destroy (gameObject);
				return;
			}
			DontDestroyOnLoad (gameObject);
		}

		public void loadLobbyScene() {
			SceneManager.LoadScene (lobbyScene);
		}
		public void loadPlayScene() {
			SceneManager.LoadScene (playScene);
		}
		public void loadItemScene() {
			SceneManager.LoadScene (itemScene);
		}
	}
}

