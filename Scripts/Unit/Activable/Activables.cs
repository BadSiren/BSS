using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.UI;

namespace BSS.Unit {
	public class Activables : SerializedMonoBehaviour
	{
		public class InitActivable
		{
			public string category;
			public int index;
			public Activable activable;
		}
		public string initCategory="Base";
        public string nowCategory {
            private set;
            get;
        }
        public int selectedAct = -1;
		public List<InitActivable> initActivableList=new List<InitActivable>();
		public Dictionary<string,List<Activable>> activableList;


        [Header("GameObject")]
        [FoldoutGroup("BaseEvent")]
        public string actChangeEvent = "ActivableUpdate";

		private const int MAX_COUNT=9;

		private BaseUnit owner;

		void Awake() {
			owner = GetComponentInParent<BaseUnit> ();
            setCategory(initCategory);
		}
		void Start() {
			foreach (var it in initActivableList) {
                registActivable(it.category, it.index, it.activable);
			}
		}

        public void setCategory(string category) {
            nowCategory = category;
            updateActivable();
        }
			
		public string getCategory(Activable activable) {
			foreach (var it in activableList) {
				var act=it.Value.Find(x => x == activable);
				if (act != null) {
					return it.Key;
				}
			}
			return "";
		}
		public int getIndex(Activable activable) {
			foreach (var it in activableList) {
				var index=it.Value.FindIndex(x => x == activable);
				if (index>=0) {
					return index;
				}
			}
			return -1;
		}
		public Activable getActivableOrNull(int index) {
            if (!activableList.ContainsKey (nowCategory)) {
				return null;
			}
            return activableList [nowCategory] [index];
		}
		public void registActivable(string category,int index,Activable act) {
            if (!act.checkDisplayable()) {
                return;
            }
			if (!activableList.ContainsKey (category)) {
				activableList [category] = new List<Activable> ();
				for (int i = 0; i < MAX_COUNT; i++) {
					activableList[category].Add(null);
				}
			}
			activableList [category][index]= act;
            updateActivable();
		}
		public void unregistActivable(string category,int index) {
            if (!activableList.ContainsKey(category)) {
                return;
            }
			Destroy (activableList [category] [index]);
			activableList [category] [index] = null;
            updateActivable();
		}

		public GameObject getContainerOrCreate(string category){
			Transform containerTr = transform.Find (category);
			if (containerTr == null) {
				var temp = new GameObject (category);
				temp.transform.SetParent (gameObject.transform);
				containerTr = temp.transform;
			}
			return containerTr.gameObject;
		}
        public void actSelect(int num) {
            selectedAct = num;
            updateActivable();
        }

        private void updateActivable() {
            var updateReacts = GetComponentsInParent<IActivableUpdateReact>();
            foreach (var it in updateReacts) {
                it.onActivableUpdate();
            }
            BaseEventListener.onPublishGameObject(actChangeEvent, owner.gameObject, owner.gameObject);
        }
	}
}

