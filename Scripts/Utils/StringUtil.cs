using UnityEngine;
using System.Collections;

namespace BSS {
	public static class StringUtil
	{
		public static string getUniqueString() {
			return System.DateTime.Now.ToString().GetHashCode().ToString("x"); 
		}

        public static string valueToString(float allValue, float initValue) {
            //Ex) allValue=9.50,initValue=3.25 => 3.25 (+6.25)
            string initText = string.Format("{0:0.##}", initValue);
            string addText = string.Format("{0:0.##}", allValue - initValue);
            return initText + " (+" + addText + ")";
        }
        public static string valueToStringFloor(float allValue, float initValue) {
            //Ex) allValue=100,initValue=20 => 20 (+80)
            string initText = string.Format("{0:0}", initValue);
            string addText = string.Format("{0:0}", allValue - initValue);
            return initText + " (+" + addText + ")";
        }
	}
}

