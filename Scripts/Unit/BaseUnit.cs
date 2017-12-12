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
	public enum UnitRelation
	{
		All, //Red,Blue,White
		My, //Red
		Enemy, //Blue
	}
	public class BaseUnit : SerializedMonoBehaviour
	{
		public static List<BaseUnit> unitList = new List<BaseUnit>();
		public static int _totalPopulation=0;
		public static int totalPopulation {
			get {
				return _totalPopulation;
			}
			set {
				_totalPopulation = value;
				BaseEventListener.onPublishInt ("TotalPopulation", _totalPopulation);
			}
		}

		public string uIndex;
		public string uName;
		public Sprite portrait;

		public UnitTeam team;
		public bool isInvincible;
		public int population;
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
		public List<Activable> activableList = new List<Activable> ();

		protected virtual void OnEnable()
		{
			unitList.Add(this);

			health = maxHealth;
			mana = maxMana;
			BaseEventListener.onPublishGameObject ("UnitCreate", gameObject);
		}
		protected virtual void OnDisable()
		{
			unitList.Remove(this);
			if (team == UnitTeam.Red) {
				totalPopulation -= population;
			}
			resetActivable ();
		}

		public virtual void allyInit() {
			team = UnitTeam.Red;
			totalPopulation += population;
			activableInitialize ();
			BaseEventListener.onPublishGameObject ("AllyInit", gameObject);
		}
		public virtual void enemyInit() {
			team = UnitTeam.Blue;
			BaseEventListener.onPublishGameObject ("EnemyInit", gameObject);
		}
			
		public void addActivable(Activable _activable) {
			activableList.Add (_activable);
		}
		public void resetActivable() {
			foreach (var it in activableList) {Destroy (it);}
			activableList.Clear ();
		}


		public bool haveProperty<T>() {
			if (GetComponent<T> () == null) {
				return false;
			}
			return true;
		}


		public virtual void die() {
			tag = "Die";
			SendMessage ("onDieEvent", SendMessageOptions.DontRequireReceiver);
			BaseEventListener.onPublishGameObject ("UnitDie", gameObject);
			Destroy (gameObject);
		}

		private void activableInitialize() {
			foreach (var it in existActivable) {
				Activable temp=BSDatabase.instance.activableDatabase.activables [it ["ID"]];
				var activable=ScriptableObject.Instantiate (temp);
				activable.initialize (it);
				addActivable (activable);
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

