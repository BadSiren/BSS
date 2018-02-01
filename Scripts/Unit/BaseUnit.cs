using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;
using System.Linq;
using Photon;

namespace BSS.Unit {
    public class BaseUnit : SerializedMonoBehaviour
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

                if (isMine && _health < 0.01f) {
                    die();
                    return;
                }
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
                return (photonView.ownerId==PhotonNetwork.player.ID || (photonView.isSceneView && PhotonNetwork.isMasterClient));
            }
        }
        public bool onlyMine {
            get {
                return (photonView.ownerId == PhotonNetwork.player.ID);
            }
        }

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
            photonView = GetComponent<PhotonView>();
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
			unitList.Remove(this);
		}

        [PunRPC]
        public void hitDamage(float _damage) {
            if (!isMine) {
                return;
            }

            if (health > _damage) {
                photonView.RPC("recvHitDamage", PhotonTargets.All,_damage);
            } else {
                photonView.RPC("die", PhotonTargets.All);
            }
        }

        [PunRPC]
        void recvHitDamage(float _damage,PhotonMessageInfo mi) {
            float damage = _damage * (1f - UnitUtils.GetDamageReduction(armor));
            health -= damage;

            var hitReacts = GetComponentsInChildren<IHitReact>();
            foreach (var it in hitReacts) {
                it.onHit();
            }
            BaseEventListener.onPublishGameObject(hitEvent, gameObject, gameObject);
        }
        [PunRPC]
        public void die() {
            if (!isMine) {
                return;
            }
            photonView.RPC("recvDie", PhotonTargets.All);
        }
        [PunRPC]
        void recvDie(PhotonMessageInfo mi) {
            var dieReacts = GetComponentsInChildren<IDieReact>();
            foreach (var it in dieReacts) {
                it.onDie();
            }
            BaseEventListener.onPublishGameObject(destroyEvent, gameObject, gameObject);
            Destroy(gameObject);
        }

    }
}

