using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BSS.Unit {
	public abstract class Upgradable : SerializedMonoBehaviour  {
		[BoxGroup("Upgradable")]
		public string ID;
		[BoxGroup("Upgradable")]
		public string listenName="UpID";
		[BoxGroup("Upgradable")]
		public ParameterType listenType=ParameterType.Int;
		[BoxGroup("Upgradable")]
		public bool startedApply = true;

		public static Dictionary<string,List<string>> dynamicApplyUnits = new Dictionary<string,List<string>> ();

		public int level {
			get {
                if (!owner.onlyMine) {
					return 0;
				}
				return GameDataBase.instance.getUpgradeLevel (ID);
			}
		}

		protected BaseUnit owner;
		private BaseEventListener listener;

		void Start() {
			initialize ();
			if (startedApply) {
				applyUpgrade (ID);
			}
		}

		public static T addComponent<T>(BaseUnit target) where T : Upgradable  {
			return target.gameObject.transform.Find ("Upgradable").gameObject.AddComponent <T>() ;
		}

		public virtual void initialize() {
			owner=GetComponentInParent<BaseUnit> ();

			listener=gameObject.AddComponent<BaseEventListener> ();
			listener.listenName = listenName;
			listener.listenType = listenType;
			listener.sendMessage = "applyUpgrade";
			listener.sendType = ParameterType.String;
			listener.isDynamic = false;
			listener.stringParameter = ID;
		}
		void OnDestroy() {
			if (listener != null) {
				Destroy (listener);
			}
		}
		public abstract void applyUpgrade (string _ID);
	}
}

