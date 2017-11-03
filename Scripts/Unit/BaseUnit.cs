using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Input;

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
		public float initArmor;
		public float armor;

		public List<Activable> activableList = new List<Activable> ();

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
			armor = initArmor;
			activableList.Capacity = 9;

			if (team != UnitTeam.Red) {
				Destroy (GetComponent<Rigidbody2D> ());
			}
		}
			
			

		public virtual void die() {
			SendMessage ("onDieEvent", health, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
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

