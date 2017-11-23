using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BSS.Play;

namespace BSS.Unit {
	public class UpAttack : Upgradable {
		private float addDamage;
		private float preChangeDamage=0f;
		private Attackable attackable;

		public override void onCreate (GameObject target,float argument)
		{
			var upAttack = ScriptableObject.Instantiate (this);
			upAttack.owner = target;
			upAttack.addDamage = argument;
			upAttack.attackable=target.GetComponent<Attackable> ();
			if (upAttack.attackable == null) {
				Destroy (upAttack);
				return;
			}
			GameDataBase.instance.addUpListener (upAttack);
			upAttack.onUpgradeApply ();
			target.SendMessage ("addUpgradable", upAttack, SendMessageOptions.DontRequireReceiver);
		}

		public override void onUpgradeApply(){
			attackable.changeDamage -= preChangeDamage;
			preChangeDamage=level * addDamage;
			attackable.changeDamage += level * addDamage;
		}
	}
}