using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS.Event {
    public class UnitCreator : SerializedMonoBehaviour
    {
        public bool isSceneObject;
        public string prefabPath;
        public Vector2 vector;
        public System.Func<Vector2> vectorFunc;

        public GameObject create() {
            var vec = vector;
            if (vectorFunc != null) {
                vec = vectorFunc();
            }
            if (isSceneObject && PhotonNetwork.isMasterClient) {
                return PhotonNetwork.InstantiateSceneObject(prefabPath, vec, Quaternion.identity, 0, null);
            } 
            return PhotonNetwork.Instantiate(prefabPath, vec, Quaternion.identity, 0);
        }

    }
}
