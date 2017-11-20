using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class UpArmor : UpBase {
		public float addArmor;
		private float preChangeArmor=0f;

		protected override void onInitialize() {
			base.onInitialize ();
			applyUpgrade();
		}
		protected override void OnDestroy() {
			owner.changeArmor -= preChangeArmor;
			upList.Remove (this);
		}

		protected override void applyUpgrade() {
			owner.changeArmor -= preChangeArmor;
			preChangeArmor=level * addArmor;
			owner.changeArmor += level * addArmor;
		}
	}
}
