using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using BSS.Play;

namespace BSS.Unit {
	public abstract class Upgradable : SerializedMonoBehaviour  {
		public string ID;
		public string listenName="UpID";
		public BaseEventListener.ParameterType listenType=BaseEventListener.ParameterType.Int;

		private BaseEventListener listener;

		public int level {
			get {
				return GameDataBase.instance.getUpgradeLevel (ID);
			}
		}
		protected BaseUnit owner;

		void Start() {
			initialize ();
		}
		public virtual void initialize() {
			owner = GetComponent<BaseUnit> ();

			listener=gameObject.AddComponent<BaseEventListener> ();
			listener.listenName = listenName;
			listener.listenType = listenType;
			listener.sendMessage = "applyUpgrade";
			listener.sendType = BaseEventListener.ParameterType.String;
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

