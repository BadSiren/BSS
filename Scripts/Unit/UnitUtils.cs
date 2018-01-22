using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS.Unit {
	public static class UnitUtils 
	{
		public static GameObject CreatePunObject(string _name,Vector2 pos) {
			var obj=PhotonNetwork.Instantiate (_name, pos, Quaternion.identity, 0);
			return obj;
		}
        public static float GetDistance(GameObject obj1,GameObject obj2) {
            return Vector2.Distance(obj1.transform.position, obj2.transform.position);
        }
        public static bool InDistance(GameObject obj1, GameObject obj2,float dis) {
            return GetDistance(obj1, obj2) < dis;
        }

        public static float GetDamageReduction(float armor) {
            return 1f - (50f / (armor + 50f));
        }
	}
}

