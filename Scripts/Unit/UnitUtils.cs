using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public static class UnitUtils 
	{
		public static bool CheckHostile(UnitTeam team,UnitTeam other) {
			if ((team == UnitTeam.White || other == UnitTeam.White) || team == other) {
				return false;
			} 
			return true;
		}
		public static bool CheckHostile(BaseUnit allyUnit,BaseUnit enemyUnit) {
			return CheckHostile (allyUnit.team, enemyUnit.team);
		}
		public static bool CheckHostile(GameObject ally,GameObject enemy) {
			BaseUnit allyUnit = ally.GetComponent<BaseUnit> ();
			BaseUnit enemyUnit = enemy.GetComponent<BaseUnit> ();
			return CheckHostile (allyUnit.team, enemyUnit.team);
		}
	}
}

