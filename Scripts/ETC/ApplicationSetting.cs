using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace BSS {
	public class ApplicationSetting : MonoBehaviour
	{

		public void loadScene(string sceneName) {
			SceneManager.LoadScene (sceneName);
		}
		public void applicationEnd() {
			Application.Quit ();
		}
	}
}

