using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using Sirenix.OdinInspector;
using System.Linq;
using Photon;

namespace BSS.Unit {
	public enum UnitTeam
	{
		Red, //Ally Team(include mine)
		Blue, //Enemy Team
		White //Neutral Team
	}
	public enum UnitRelation
	{
		All, //Red,Blue,White
		My, //Red
		Enemy, //Blue
	}
	public class BaseUnit : SerializedMonoBehaviour
	{
		public static List<BaseUnit> unitList=new List<BaseUnit>();

		public string uIndex;
		public string uName;
		public Sprite portrait;

		public UnitTeam team;
		public bool isInvincible;
		public float maxHealth;
		public float health;
		public float maxMana;
		public float mana;
		[SerializeField]
		private float _initArmor;
		public float initArmor {
			get { 
				return _initArmor;
			}
		}
		[HideInInspector]
		public float changeArmor=0f;
		public float armor {
			get {
				return initArmor+changeArmor;
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
			
		public PhotonView photonView {
			get;
			private set;
		}
		public Activables activables {
			get;
			private set;
		}
		[BoxGroup("Event(GameObject)")]
		public string enableEvent="UnitEnable";
		[BoxGroup("Event(GameObject)")]
		public string destroyEvent="UnitDestroy";

		void Awake() {
			photonView = GetComponent<PhotonView> ();
			activables = GetComponentInChildren<Activables> ();
		}
		protected virtual void OnEnable()
		{
			unitList.Add(this);


			health = maxHealth;
			mana = maxMana;
			BaseEventListener.onPublishGameObject (enableEvent, gameObject, gameObject);
		}
		protected virtual void OnDisable()
		{
			BaseEventListener.onPublishGameObject (destroyEvent, gameObject, gameObject);
			unitList.Remove(this);
		}

		public virtual void die() {
			tag = "Die";
			SendMessage ("onDieEvent", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
			
			
		private void hitDamage(AttackInfo attackInfo) {
			float _damage = attackInfo.damage * (1f - reductionArmor (armor));
			health -= _damage;
			if (health < 0.1f) {
				attackInfo.attacker.SendMessage ("onKillEvent",gameObject, SendMessageOptions.DontRequireReceiver);
				die ();
			}
			SendMessage ("AssignHealthValue", health, SendMessageOptions.DontRequireReceiver);

		}
		public static float reductionArmor(float _armor) {
			return 1f-(50f / (_armor + 50f));
		}
			

		//Unit Event
		private void onHitEvent(AttackInfo attackInfo) {
			hitDamage (attackInfo);
		}

	}
}

