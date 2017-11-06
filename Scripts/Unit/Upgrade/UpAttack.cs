using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS.Unit {
	public class UpAttack : UpBase {
		public float addDamage;

		private Attackable attackable;
		private float preChangeDamage=0f;

		protected override void onInitialize() {
			base.onInitialize ();
			attackable = gameObject.GetComponent<Attackable> ();
			if (attackable == null) {
				Destroy (this);
			}
			applyUpgrade();
		}
		protected override void OnDestroy() {
			attackable.changeDamage -= preChangeDamage;
			upList.Remove (this);
		}

		protected override void applyUpgrade() {
			attackable.changeDamage -= preChangeDamage;
			preChangeDamage=level * addDamage;
			attackable.changeDamage += level * addDamage;
		}
	}
}