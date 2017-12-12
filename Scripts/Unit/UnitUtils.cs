using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public static class UnitUtils 
	{
		public static List<BaseUnit> GetUnitsInCircle(Vector2 orgin,float radius) {
			var colls = Physics2D.OverlapCircleAll (orgin,radius);
			List<BaseUnit> units = new List<BaseUnit> ();
			foreach (var col in colls) {
				if (!(col is BoxCollider2D)) {
					continue;
				}
				var unit=col.gameObject.GetComponent<BaseUnit> ();
				if (unit != null) {
					units.Add (unit);
				}
			}
			return units;
		}
		public static List<BaseUnit> GetUnitsInCircle(Vector2 orgin,float radius,UnitTeam team) {
			return GetUnitsInCircle(orgin,radius).FindAll(x=>x.team==team);
		}
		public static List<BaseUnit> GetEnemiesInCircle(Vector2 orgin,float radius,UnitTeam team) {
			return GetUnitsInCircle (orgin, radius, GetHostile (team));
		}
		public static bool IsUnitInCircle(BaseUnit unit,Vector2 orgin,float radius) {
			return GetUnitsInCircle (orgin, radius).Contains (unit);
		}
		public static List<GameObject> GetObjectsInCircle(Vector2 orgin,float radius) {
			return GetUnitsInCircle (orgin,radius).ConvertAll (x => x.gameObject);
		}
		public static bool IsObjectInCircle(GameObject obj,Vector2 orgin,float radius) {
			return GetObjectsInCircle (orgin, radius).Contains (obj);
		}

		public static UnitTeam GetHostile(UnitTeam team) {
			if (team == UnitTeam.Red) {
				return UnitTeam.Blue;
			} 
			if (team == UnitTeam.Blue) {
				return UnitTeam.Red;
			} 
			return UnitTeam.White;
		}
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

		public static GameObject CreateUnit(BaseUnit unit,Vector3 pos,UnitTeam _team) {
			GameObject obj=GameObject.Instantiate (unit.gameObject, pos, Quaternion.identity);
			BaseUnit _unit = obj.GetComponent<BaseUnit> ();
			_unit.team = _team;
			if (_unit.team == UnitTeam.Red) {
				_unit.allyInit ();
			} else if (_unit.team == UnitTeam.Blue) {
				_unit.enemyInit ();
			}
			return obj;
		}
		public static GameObject CreateUnit(GameObject unitObject,Vector3 pos,UnitTeam _team) {
			GameObject obj=GameObject.Instantiate (unitObject, pos, Quaternion.identity);
			BaseUnit _unit = obj.GetComponent<BaseUnit> ();
			_unit.team = _team;
			if (_unit.team == UnitTeam.Red) {
				_unit.allyInit ();
			} else if (_unit.team == UnitTeam.Blue) {
				_unit.enemyInit ();
			}
			return obj;
		}
	}
}

