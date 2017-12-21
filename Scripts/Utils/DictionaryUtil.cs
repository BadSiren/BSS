using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS {
	public static class DictionaryUtil
	{

		public static List<string> getKeyListInStartWith<T>(Dictionary<string,T> dic,string startString) {
			List<string> returnList = new List<string> ();
			foreach (var it in dic.Keys) {
				if (it.StartsWith (startString)) {
					returnList.Add (it);
				}
			}
			return returnList;
		}
	}
}
