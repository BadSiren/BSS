using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS.Unit {
	public class UpAttack : Upgradable {
		[Header("UpAttack")]
		public float addDamage;
		private float preChangeDamage=0f;
		private Attackable attackable;

		public override void initialize ()
		{
			base.initialize ();
			attackable=owner.gameObject.GetComponent<Attackable> ();
			if (attackable == null) {
				Destroy (this);
				return;
			}
		}
		 
		public override void applyUpgrade(string _ID){
			if (ID != _ID) {
				return;
			}
			attackable.changeDamage += (level * addDamage)-preChangeDamage;
			preChangeDamage=level * addDamage;
		}
	}
}