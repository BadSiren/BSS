using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace BSS {
    public class PhotonTool : MonoBehaviour
    {
        public string sceneName;
        public void loomLeaveAndLoadScene() {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(sceneName);
        }
    }
}