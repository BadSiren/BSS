using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;
using BSS.Skill;

namespace BSS.Unit {
	public enum UnitTeam
	{
		Red, //Ally Team(include mine)
		Blue, //Enemy Team
		White //Neutral Team
	}
	public class BaseUnit : MonoBehaviour
	{
		public static List<BaseUnit> unitList = new List<BaseUnit>();

		public string uIndex;
		public string uName;

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

		public List<string> activableText = new List<string> ();
		public List<Activable> activableList = new List<Activable> ();
		public List<Skillable> skillList = new List<Skillable> ();
		public List<GameObject> detectedUnits =new List<GameObject> ();




		public virtual void die() {
			tag = "Die";
			SendMessage ("onDieEvent", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}

		public virtual void setEnemy() {
			team = UnitTeam.Blue;

			foreach (var it in GetComponents<UpBase> ()) {
				Destroy (it);
			}
		}
		public void addActivable(string actIndex) {
			var act=ActivableDatabase.instance.activableDatabaseDic [actIndex];
			if (act == null) {
				return;
			}
			activableList.Add (act);
		}

		protected virtual void OnEnable()
		{
			unitList.Add(this);
			initialize ();
		}
		protected virtual void OnDisable()
		{
			unitList.Remove(this);
		}
		protected virtual void initialize() {
			health = maxHealth;
			mana = maxMana;
			actInitialize ();

			skillInitialize ();

			SendMessage ("onInitialize", SendMessageOptions.DontRequireReceiver);
		}
		private void actInitialize() {
			activableList.Capacity = 9;
			foreach (var it in activableText) {
				addActivable (it);
			}
		}

		private void skillInitialize() {
			foreach (var it in skillList) {
				it.addComponent (gameObject);
			}
		}
			
		private void hitDamage(AttackInfo attackInfo) {
			float _damage = attackInfo.damage * (1f - reductionCalc (armor));
			health -= _damage;
			if (health < 0.1f) {
				attackInfo.attacker.SendMessage ("onKillEvent",gameObject, SendMessageOptions.DontRequireReceiver);
				die ();
			}
			SendMessage ("AssignHealthValue", health, SendMessageOptions.DontRequireReceiver);
		}
		private float reductionCalc(float _armor) {
			return 1f-(100f / (_armor + 100f));
		}


		//Unit Event
		private void onHitEvent(AttackInfo attackInfo) {
			hitDamage (attackInfo);
		}

	}
}

