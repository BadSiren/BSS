using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using BSS.Level;

namespace BSS {
	public class LoadBase : MonoBehaviour
	{
		public static LoadBase instance;

		public string loadScene="PlayScene";
		public int startMoney;
		public int startFood;

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

		public void loadPlayScene() {
			SceneManager.LoadScene (loadScene);
		}
	}
}

