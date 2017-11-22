using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BSS {
	public class BaseEventListener : MonoBehaviour
	{
		public enum ListenType
		{
			Void,GameObject,Int,String
		}

		public static List<BaseEventListener> eventListeners = new List<BaseEventListener> ();
		public ListenType type;
		public string listenName;
		public string sendMessage;

		void Awake() {
			if (!eventListeners.Contains (this)) {
				eventListeners.Add (this);
			}

		}
		void OnDestroy() {
			if (eventListeners.Contains (this)) {
				eventListeners.Remove (this);
			}
		}

		public static void onPublish(string _listenType) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.type==ListenType.Void);
			foreach (var it in listeners) {
				it.gameObject.SendMessage (it.sendMessage, SendMessageOptions.DontRequireReceiver);
			}
		}
		public static void onPublishGameObject(string _listenType,GameObject param) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.type==ListenType.GameObject);
			foreach (var it in listeners) {
				it.gameObject.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
			}
			onPublish (_listenType);
		}
		public static void onPublishInt(string _listenType,int param) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.type==ListenType.Int);
			foreach (var it in listeners) {
				it.gameObject.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
			}
			onPublish (_listenType);
		}
		public static void onPublishString(string _listenType,string param) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.type==ListenType.String);
			foreach (var it in listeners) {
				it.gameObject.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
			}
			onPublish (_listenType);
		}

	}
}

