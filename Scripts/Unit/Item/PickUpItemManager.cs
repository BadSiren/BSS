using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
    public class PickUpItemManager : SerializedMonoBehaviour
    {
        public static PickUpItemManager instance;
        public GameObject pickUpItemPrefab;
        public List<PickUpItem> pickUpItems = new List<PickUpItem>();

        private PhotonView photonView;

        void Awake() {
            instance = this;
            photonView = GetComponent<PhotonView>();
        }

        public void create(string ID, Vector2 pos) { 
            photonView.RPC("recvCreate", PhotonTargets.All, ID, pos);
        }
        [PunRPC]
        void recvCreate(string ID, Vector2 pos,PhotonMessageInfo mi) {
            var obj = Instantiate(pickUpItemPrefab, pos, Quaternion.identity);
            var item = obj.GetComponent<PickUpItem>();
            item.ID = ID;
            pickUpItems.Add(item);
        }
        public void destroy(int num) {
            photonView.RPC("recvDestroy", PhotonTargets.All, num);
        }
        [PunRPC]
        void recvDestroy(int num, PhotonMessageInfo mi) {
            var item = pickUpItems[num];
            pickUpItems.RemoveAt(num);
            Destroy(item.gameObject);
        }
    }
}
