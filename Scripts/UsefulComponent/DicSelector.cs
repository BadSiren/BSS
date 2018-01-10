using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
	public class DicSelector : SerializedMonoBehaviour
	{
		public static List<DicSelector> dicSelectorList = new List<DicSelector> ();
		public string ID = "";

		[Header("DicProb(DefineKey)")]
		[InlineProperty()]
		public List<Dictionary<string,string>> orginDics;

		public bool isCustomDic=false;
		[ShowIf("isCustomDic")]
		public CustomDictionary customDic;

		private List<Dictionary<string,string>> dics;


		void Awake() {
			dicSelectorList.Add (this);
			dics = new List<Dictionary<string,string>> (orginDics);
		}
		void OnDestroy() {
			dicSelectorList.Remove (this);
		}

		public void resetDics() {
			dics=new List<Dictionary<string,string>> (orginDics);
		}

		public Dictionary<string,string> getDic(int num) {
			return dics [num];
		}
		public Dictionary<string,string> getDicWithRand() {
			return dics [Random.Range (0, dics.Count)];
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

		public Sprite convertSprite(string value) {
			return customDic.getSprite (value);
		}
		public string convertText(string value) {
			return customDic.getText (value);
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
