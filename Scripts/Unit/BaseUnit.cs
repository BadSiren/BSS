using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;
using System.Linq;
using Photon;

namespace BSS.Unit {
    public class BaseUnit : SerializedMonoBehaviour,IPunObservable
	{
		public static List<BaseUnit> unitList=new List<BaseUnit>();

		public string uIndex;
		public string uName;
		public Sprite portrait;

		public bool isInvincible;
        [SerializeField]
        private float _maxHealth;
        public float maxHealth {
            get {
                return _maxHealth;
            }
            set {
                _maxHealth = value;
            }
        }
        private float _health;
        public float health {
            get {
                return _health;
            }
            set {
                _health = value;
            }
        }
		public float maxMana;
		public float mana;

        public float initArmor;
        [HideInInspector]
        public float changeArmor = 0f;
        public float armor {
            get {
                return initArmor + changeArmor;
            }
        }
		
		public bool isMine {
			get {
				return photonView.ownerId == PhotonNetwork.player.ID;
			}
		}
		public bool isSceneObject {
			get {
				return photonView.owner == null;
			}
		}
		
        [SerializeField]
		public PhotonView photonView {
			get;
			private set;
		}
		public Activables activables {
			get;
			private set;
		}


        [Header("GaemObject")]
        [FoldoutGroup("BaseEvent")]
        public string enableEvent = "UnitEnable";
        [FoldoutGroup("BaseEvent")]
        public string destroyEvent = "UnitDestroy";
        [FoldoutGroup("BaseEvent")]
        public string hitEvent = "UnitHit";


		void Awake() {
			activables = GetComponentInChildren<Activables> ();
		}
		protected virtual void OnEnable()
		{
			unitList.Add(this);

            maxHealth = _maxHealth;
			health = maxHealth;
			mana = maxMana;
			BaseEventListener.onPublishGameObject (enableEvent, gameObject, gameObject);
		}
		protected virtual void OnDisable()
		{
			BaseEventListener.onPublishGameObject (destroyEvent, gameObject, gameObject);
			unitList.Remove(this);
		}

        public void hitDamage(float damage) {
            photonView.RPC("recvHitDamage",PhotonTargets.All,damage);
        }

        [PunRPC]
        void recvHitDamage(float _damage,PhotonMessageInfo mi) {
            if (isMine) {
                float damage = _damage * (1f - UnitUtils.GetDamageReduction(armor));
                health -= damage;
                if (health < 0.01f) {
                    destroy();
                    return;
                } 
            }

            var hitReacts = GetComponents<IHitReact>();
            foreach (var it in hitReacts) {
                it.onHit();
            }
            BaseEventListener.onPublishGameObject(hitEvent, gameObject, gameObject);
        }
        public void destroy() {
            photonView.RPC("recvDestroy", PhotonTargets.All);
        }
        [PunRPC]
        void recvDestroy(PhotonMessageInfo mi) {
            BaseEventListener.onPublishGameObject(destroyEvent, gameObject, gameObject);
            Destroy(gameObject);
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.isWriting && isMine) {
                // We own this player: send the others our data
                stream.SendNext(health);
            } else {
                // Network player, receive data
                health = (float)stream.ReceiveNext();
            }
        }

    }
}

