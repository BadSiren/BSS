using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace BSS {
	public class ApplicationSetting : MonoBehaviour
	{
		public string sceneName;

		public void loadScene() {
			SceneManager.LoadScene (sceneName);
		}
		public void applicationEnd() {
			Application.Quit ();
		}
	}
}

