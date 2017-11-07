using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Skill {
	public class PsRangeUp : PassiveSkill
	{
		public float initRange;
		public float levelRange;

		private Attackable attackable;
		private float preChangeRange=0f;

		protected void onInitialize() {
			base.onInitialize ();
			attackable = gameObject.GetComponent<Attackable> ();
			if (attackable == null) {
				Destroy (this);
			}
			initApply ();
		}
		protected override void replaceTexting() {
			base.replaceTexting ();
			replaceText = replaceText.Replace ("@range", (initRange + levelRange * (level - 1)).ToString());
		}

		private void initApply() {
			if (!validate ()) {
				return;
			}
			preChangeRange=initRange + levelRange * (level - 1);
			attackable.changeRange += initRange + levelRange * (level - 1);
		}


		void OnDestroy() {
			attackable.changeRange -= preChangeRange;
		}
	}
}

