using UnityEngine;
using System.Collections;

namespace BSS {
    public class PhotonManager : Photon.MonoBehaviour
    {
        public static PhotonManager instance;

        void Awake() {
            instance=this;
        }

    }
}
