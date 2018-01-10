using UnityEngine;
using System.Collections;

namespace BSS {
	public static class StringUtil
	{
		public static string getUniqueString() {
			return System.DateTime.Now.ToString().GetHashCode().ToString("x"); 
		}
	}
}

