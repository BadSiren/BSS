using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS.Unit {
    public class PickUpItemCreator : SerializedMonoBehaviour {
        public string prefabPath = "PunObject/PickUpItem";
        public Vector2 vector;
        public System.Func<Vector2> vectorFunc;
        public string ID;

        public GameObject create() {
            Vector2 vec = vector;
            if (vectorFunc != null) {
                vec = vectorFunc();
            }
            var obj = PhotonNetwork.InstantiateSceneObject(prefabPath, vectorFunc(), Quaternion.identity, 0, null);
            var pickUpItem = obj.GetComponent<PickUpItem>();
            pickUpItem.ID = ID;
            return obj;
        }
        public void createReturnVoid() {
            Vector2 vec = vector;
            if (vectorFunc != null) {
                vec = vectorFunc();
            }
            var obj = PhotonNetwork.InstantiateSceneObject(prefabPath, vectorFunc(), Quaternion.identity, 0, null);
            var pickUpItem = obj.GetComponent<PickUpItem>();
            pickUpItem.ID = ID;
        }
        public static GameObject Create(string _ID,Vector2 pos,string path="PunObject/PickUpItem") {
            var obj = PhotonNetwork.InstantiateSceneObject(path, pos, Quaternion.identity, 0, null);
            var pickUpItem = obj.GetComponent<PickUpItem>();
            pickUpItem.ID = _ID;
            return obj;
        }

    }
}