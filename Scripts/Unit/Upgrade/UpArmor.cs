using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class UpArmor : UpBase {
		public float addArmor;

		private BaseUnit baseUnit;
		private float preChangeArmor=0f;

		protected override void onInitialize() {
			base.onInitialize ();
			baseUnit = gameObject.GetComponent<BaseUnit> ();
			if (baseUnit == null) {
				Destroy (this);
			}
			applyUpgrade();
		}
		protected override void OnDestroy() {
			baseUnit.changeArmor -= preChangeArmor;
			upList.Remove (this);
		}

		protected override void applyUpgrade() {
			baseUnit.changeArmor -= preChangeArmor;
			preChangeArmor=level * addArmor;
			baseUnit.changeArmor += level * addArmor;
		}
	}
}
