using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
	public class BaseEventListener : SerializedMonoBehaviour
	{
		public enum ParameterType
		{
			Void,GameObject,Int,String
		}
		public static List<BaseEventListener> eventListeners = new List<BaseEventListener> ();

		public string listenName;
		public ParameterType listenType;

		public string sendMessage;
		public bool isDynamic=true;
		[Header("StaticParameter")]
		public ParameterType sendType;
		public GameObject gameObjectParameter;
		public int intParameter;
		[TextArea()]
		public string stringParameter;

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
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.Void);
			foreach (var it in listeners) {
				if (it.isDynamic) {
					it.gameObject.SendMessage (it.sendMessage, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it);
				}
			}
		}
		public static void onPublishGameObject(string _listenType,GameObject param) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.GameObject);
			foreach (var it in listeners) {
				if (it.isDynamic) {
					it.gameObject.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it);
				}
			}
		}
		public static void onPublishInt(string _listenType,int param) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.Int);
			foreach (var it in listeners) {
				if (it.isDynamic) {
					it.gameObject.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it);
				}
			}
		}
		public static void onPublishString(string _listenType,string param) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.String);
			foreach (var it in listeners) {
				if (it.isDynamic) {
					it.gameObject.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it);
				}
			}
		}
		public static void sendStaticEvent(BaseEventListener _listener){
			switch (_listener.sendType) {
			case ParameterType.Void:
				_listener.gameObject.SendMessage (_listener.sendMessage, SendMessageOptions.DontRequireReceiver);
				break;
			case ParameterType.GameObject:
				_listener.gameObject.SendMessage (_listener.sendMessage,_listener.gameObjectParameter, SendMessageOptions.DontRequireReceiver);
				break;
			case ParameterType.Int:
				_listener.gameObject.SendMessage (_listener.sendMessage,_listener.intParameter, SendMessageOptions.DontRequireReceiver);
				break;
			case ParameterType.String:
				_listener.gameObject.SendMessage (_listener.sendMessage,_listener.stringParameter, SendMessageOptions.DontRequireReceiver);
				break;
			}
		}

	}
}

