using UnityEngine;
using System.Collections;

namespace BSS.Event {
    public class PhotonCondition : MonoBehaviour
    {
        public bool isMasterClient() {
            return PhotonNetwork.isMasterClient;
        }
    }
}