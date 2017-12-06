using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using BSS.Skill;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public enum UnitTeam
	{
		Red, //Ally Team(include mine)
		Blue, //Enemy Team
		White //Neutral Team
	}
	public class BaseUnit : SerializedMonoBehaviour
	{
		public static List<BaseUnit> unitList = new List<BaseUnit>();

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
		public List<Dictionary<string,string>> existActivable = new List<Dictionary<string,string>>();
		public Dictionary<string,float> existUpgradables = new Dictionary<string,float> ();

		public List<Activable> activableList = new List<Activable> ();
		public List<Upgradable> upgradableList= new List<Upgradable>();

		public virtual void die() {
			tag = "Die";
			SendMessage ("onDieEvent", SendMessageOptions.DontRequireReceiver);
			BaseEventListener.onPublishGameObject ("UnitDie", gameObject);
			Destroy (gameObject);
		}

		public virtual void setEnemy() {
			team = UnitTeam.Blue;
			resetActivable ();
			resetUpgradable ();
			BaseEventListener.onPublishGameObject ("SetEnemy", gameObject);
		}
		public void addActivable(Activable _activable) {
			activableList.Add (_activable);
		}
		public void resetActivable() {
			foreach (var it in activableList) {Destroy (it);}
			activableList.Clear ();
		}
		public void addUpgradable(Upgradable _upgradable) {
			upgradableList.Add (_upgradable);
		}
		public void addUpgradable(string upID,float argument) {
			BSDatabase.instance.baseUnitDatabase.upgrades [upID].onCreate (gameObject, argument);
		}
		public void resetUpgradable() {
			foreach (var it in upgradableList) {Destroy (it);}
			upgradableList.Clear ();
		}

		public bool haveProperty<T>() {
			if (GetComponent<T> () == null) {
				return false;
			}
			return true;
		}

		protected virtual void OnEnable()
		{
			unitList.Add(this);
			initialize ();
		}
		protected virtual void OnDisable()
		{
			unitList.Remove(this);
			resetActivable ();
			resetUpgradable ();
		}

		protected virtual void initialize() {
			health = maxHealth;
			mana = maxMana;

			activableInitialize ();
			upgradeInitialize ();

			SendMessage ("onInitialize", SendMessageOptions.DontRequireReceiver);
			BaseEventListener.onPublishGameObject ("UnitCreate", gameObject);
		}
			
		private void activableInitialize() {
			foreach (var it in existActivable) {
				Activable temp=BSDatabase.instance.activableDatabase.activables [it ["ID"]];
				var activable=ScriptableObject.Instantiate (temp);
				activable.initialize (it);
				addActivable (activable);
			}
		}
		private void upgradeInitialize() {
			foreach (var it in existUpgradables) {
				addUpgradable (it.Key, it.Value);
			}
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

