using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
	public class DicSelector : SerializedMonoBehaviour
	{
		[Header("DicProb(DefineKey)")]
		public List<Dictionary<string,string>> orginDics;

		[SerializeField]
		private List<Dictionary<string,string>> dics;

		void Awake() {
			dics = new List<Dictionary<string,string>> (orginDics);
		}

		public void resetDics() {
			dics=new List<Dictionary<string,string>> (orginDics);
		}

		public Dictionary<string,string> popDicWithRand() {
			return popDic(dics,Random.Range (0, dics.Count));
		}
		public Dictionary<string,string> popDicWithRand(System.Predicate<Dictionary<string,string>> condition) {
			var conditionDics = dics.FindAll (condition);
			return popDic(conditionDics,Random.Range (0, conditionDics.Count));
		}

		public Dictionary<string,string> popDicWithProb() {
			int randomInt=chooseNum (dics.ConvertAll (x => float.Parse (x ["DicProb"])));
			return popDic(dics,randomInt);
		}

		public Dictionary<string,string> copyDicWithRand() {
			return new Dictionary<string,string>(dics[Random.Range(0,dics.Count)]);
		}
		public Dictionary<string,string> copyDicWithProb() {
			int randomInt=chooseNum (dics.ConvertAll (x => float.Parse (x ["DicProb"])));
			return new Dictionary<string,string> (dics [randomInt]);
		}



		private Dictionary<string,string> popDic(List<Dictionary<string,string>> _dics,int _index) {
			var returnDic=_dics [_index];
			dics.RemoveAt (_index);
			return returnDic;
		}

		private int chooseNum (List<float> probs) {
			if (probs == null || probs.Count==0) {
				return 0;
			}
			float total = 0;
			foreach (var it in probs) {
				total += it;
			}
			float randomPoint = Random.value * total;

			for (int i= 0; i < probs.Count; i++) {
				if (randomPoint < probs[i]) {
					return i;
				}
				else {
					randomPoint -= probs[i];
				}
			}
			return probs.Count - 1;
		}
	}
}
