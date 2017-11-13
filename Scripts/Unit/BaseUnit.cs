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
		public List<GameObject> detectedUnits =new List<GameObject> ();

		private const float SIGHT=20f;
		private DetectCollider detectCollider;


		public virtual void die() {
			tag = "Die";
			SendMessage ("onDieEvent", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}

		public virtual void setEnemy() {
			team = UnitTeam.Blue;

			Rigidbody2D body = GetComponent<Rigidbody2D> ();
			if (body != null) {
				//body.bodyType = RigidbodyType2D.Kinematic;
			}

			Collider2D col = GetComponent<Collider2D> ();
			if (col != null) {
				//col.isTrigger = true;
			}
			foreach (var it in GetComponents<UpBase> ()) {
				Destroy (it);
			}
		}
		public List<GameObject> getTargets() {
			Attackable attackable = GetComponent<Attackable> ();
			if (attackable == null) {
				return null;
			}
			return attackable.targets;
		}
		public List<GameObject> getReserveTargets() {
			Attackable attackable = GetComponent<Attackable> ();
			if (attackable == null) {
				return null;
			}
			return attackable.reserveTargets;
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
			activableList.Capacity = 9;
			initDetectCollider ();
			skillInitialize ();

			SendMessage ("onInitialize", SendMessageOptions.DontRequireReceiver);
		}
		private void initDetectCollider() {
			GameObject obj = new GameObject ("DetectRange");
			obj.transform.SetParent (gameObject.transform,false);
			detectCollider=obj.AddComponent <DetectCollider>();
			detectCollider.baseUnit = this;
			detectCollider.setRadius (SIGHT);
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
		private bool checkHostile(UnitTeam team,UnitTeam other) {
			if ((team == UnitTeam.White || other == UnitTeam.White) || team == other) {
				return false;
			} 
			return true;
		}
		private bool checkHostile(BaseUnit enemyUnit) {
			return checkHostile (team, enemyUnit.team);
		}

		//Enemy Detected
		public void OnTriggerEnterDetected(Collider2D col) {
			if (tag=="Ignore" ||col is CircleCollider2D) {
				return;
			}
			BaseUnit unit=col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || unit.isInvincible || !checkHostile(unit))
			{
				return;
			}
			if (!detectedUnits.Contains (unit.gameObject)) {
				detectedUnits.Add (unit.gameObject);
				SendMessage ("onEnterDetectedEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				if (detectedUnits.Count == 1) {
					SendMessage ("onFirstDetectedEvent", col.gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		public void OnTriggerExitDetected(Collider2D col) {
			/*
			if (tag=="Ignore" ||  col is CircleCollider2D) {
				return;
			}
			BaseUnit unit=col.gameObject.GetComponent<BaseUnit> ();
			if (unit == null || unit.isInvincible || !checkHostile(unit))
			{
				return;
			}
			*/
			if (detectedUnits.Contains (col.gameObject)) {
				detectedUnits.Remove (col.gameObject);
				SendMessage ("onExitDetectedEvent",col.gameObject, SendMessageOptions.DontRequireReceiver);
				if (detectedUnits.Count == 0) {
					SendMessage ("onNothingDetectedEvent", SendMessageOptions.DontRequireReceiver);
				}
			}
		}


		//Unit Event
		private void onHitEvent(AttackInfo attackInfo) {
			hitDamage (attackInfo);
		}

	}
}

