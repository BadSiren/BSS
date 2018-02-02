using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;
using System.Linq;
using Photon;

namespace BSS.Unit {
    public class BaseUnit : SerializedMonoBehaviour,IItemUseReact
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
                if (healthBar != null) {
                    healthBar.AssignMaxHealthValue(_maxHealth);
                }
            }
        }
        private float _health;
        public float health {
            get {
                return _health;
            }
            set {
                _health = value;
                if (healthBar != null) {
                    healthBar.AssignHealthValue(_health);
                }
                if (isMine) {
                    if (_health < 0.01f) {
                        photonView.RPC("recvDie", PhotonTargets.All);
                    } else {
                        photonView.RPC("recvHealth", PhotonTargets.Others, _health);
                    }
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

        private HealthBar _healthBar;
        public HealthBar healthBar {
            get {
                if (_healthBar == null) {
                    _healthBar = GetComponentInChildren<HealthBar>();
                }
                return _healthBar;
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

        [FoldoutGroup("ItemUseAct")]
        public string healthUseAct = "HealthRestore";


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
        public void recvHealth(float changeHealth,PhotonMessageInfo mi) {
            health = changeHealth;
        }

        [PunRPC]
        public void hitDamage(float _damage) {
            if (isMine) {
                float damage = _damage * (1f - UnitUtils.GetDamageReduction(armor));
                health -= damage;
            }
            var hitReacts = GetComponentsInChildren<IHitReact>();
            foreach (var it in hitReacts) {
                it.onHit();
            }
            BaseEventListener.onPublishGameObject(hitEvent, gameObject, gameObject);
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
        //Interface
        public void onItemUse(string ID, float _value) {
            if (ID != healthUseAct) {
                return;
            }
            health += _value;
        }
    }
}

