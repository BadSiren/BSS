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
		public float armor {
			get {
				return initArmor;
			}
		}

		public List<Activable> activableList = new List<Activable> ();
		public List<Skillable> skillList = new List<Skillable> ();

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
			activableList.Capacity = 9;
			skillInitialize ();

			SendMessage ("onInitialize", SendMessageOptions.DontRequireReceiver);
		}
		private void skillInitialize() {
			foreach (var it in skillList) {
				it.addComponent (gameObject);
			}
		}
			

		public virtual void die() {
			SendMessage ("onDieEvent", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}

		public virtual void setEnemy() {
			team = UnitTeam.Blue;
			Destroy (GetComponent<Rigidbody2D> ());
			foreach (var it in GetComponents<UpBase> ()) {
				Destroy (it);
			}
		}

		protected virtual void onHitEvent(AttackInfo attackInfo) {
			hitDamage (attackInfo);
		}

		protected void hitDamage(AttackInfo attackInfo) {
			float _damage = attackInfo.damage * (1f - reductionCalc (armor));
			health -= _damage;
			if (health <= 0f) {
				die ();
			}
			SendMessage ("AssignHealthValue", health, SendMessageOptions.DontRequireReceiver);
		}

		private float reductionCalc(float _armor) {
			return 1f-(100f / (_armor + 100f));
		}

	}
}

