using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Play;

namespace BSS.Unit {
	public class UpHealth : Upgradable {
		[Header("UpHealth")]
		public float addHealth;
		private float preChangeHealth=0f;

		public override void applyUpgrade(string _ID){
			if (owner.team!=UnitTeam.Red || ID != _ID) {
				return;
			}
			owner.maxHealth += (level * addHealth)-preChangeHealth;
			owner.health+=(level * addHealth)-preChangeHealth;
			preChangeHealth=level * addHealth;
		}
	}
}
