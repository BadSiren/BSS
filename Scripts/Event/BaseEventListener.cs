using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS {

	public class BaseEventListener : SerializedMonoBehaviour
	{
		public enum ParameterType
		{
			Void,GameObject,Int,String
		}

		public static List<BaseEventListener> eventListeners = new List<BaseEventListener> ();
		public string eventName="";
		[FoldoutGroup("Condition")]
		public Condition thisCondition;
		[FoldoutGroup("Condition")]
		public Condition publisherCondition;
		[FoldoutGroup("Listen")]
		public string listenName;
		[FoldoutGroup("Listen")]
		public ParameterType listenType;
		[FoldoutGroup("Send")]
		public bool isSelf=true;
		[HideIf("isSelf")]
		[FoldoutGroup("Send")]
		public GameObject sender;
		[FoldoutGroup("Send")]
		public string sendMessage;
		[FoldoutGroup("Send")]
		public bool isDynamic=true;
		[FoldoutGroup("Send")]
		[Header("StaticParameter")]
		[HideIf("isDynamic")]
		public ParameterType sendType;
		[FoldoutGroup("Send")]
		[HideIf("isDynamic")]
		public GameObject gameObjectParameter;
		[FoldoutGroup("Send")]
		[HideIf("isDynamic")]
		public int intParameter;
		[FoldoutGroup("Send")]
		[HideIf("isDynamic")]
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

		public static void onPublish(string _listenType,GameObject publisher=null) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.Void);
			foreach (var it in listeners) {
				if (it.thisCondition!=null && !it.thisCondition.validate (it.gameObject)) {
					continue;
				}
				if (publisher != null && it.publisherCondition!=null && !it.publisherCondition.validate (publisher)) {
					continue;
				}
				var _sender = it.gameObject;
				if (it.sender != null) {
					_sender = it.sender;
				} 
				if (it.isDynamic) {
					_sender.SendMessage (it.sendMessage, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it,_sender);
				}
			}
		}

		public static void onPublishGameObject(string _listenType,GameObject param,GameObject publisher=null) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.GameObject);
			foreach (var it in listeners) {
				if (it.thisCondition!=null && !it.thisCondition.validate (it.gameObject)) {
					continue;
				}
				if (publisher != null && it.publisherCondition!=null && !it.publisherCondition.validate (publisher)) {
					continue;
				}
				var _sender = it.gameObject;
				if (it.sender != null) {
					_sender = it.sender;
				} 
				if (it.isDynamic) {
					_sender.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it,_sender);
				}
			}
		}
		public static void onPublishInt(string _listenType,int param,GameObject publisher=null) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.Int);
			foreach (var it in listeners) {
				if (it.thisCondition!=null && !it.thisCondition.validate (it.gameObject)) {
					continue;
				}
				if (publisher != null && it.publisherCondition!=null && !it.publisherCondition.validate (publisher)) {
					continue;
				}
				var _sender = it.gameObject;
				if (it.sender != null) {
					_sender = it.sender;
				} 
				if (it.isDynamic) {
					_sender.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it,_sender);
				}
			}
		}
		public static void onPublishString(string _listenType,string param,GameObject publisher=null) {
			var listeners=eventListeners.FindAll (x => x.listenName == _listenType && x.listenType==ParameterType.String);
			foreach (var it in listeners) {
				if (it.thisCondition!=null && !it.thisCondition.validate (it.gameObject)) {
					continue;
				}
				if (publisher != null && it.publisherCondition!=null && !it.publisherCondition.validate (publisher)) {
					continue;
				}
				var _sender = it.gameObject;
				if (it.sender != null) {
					_sender = it.sender;
				} 
				if (it.isDynamic) {
					_sender.SendMessage (it.sendMessage,param, SendMessageOptions.DontRequireReceiver);
				} else {
					sendStaticEvent (it,_sender);
				}
			}
		}
	

		public static void sendStaticEvent(BaseEventListener _listener,GameObject _sender){
			switch (_listener.sendType) {
			case ParameterType.Void:
				_sender.SendMessage (_listener.sendMessage, SendMessageOptions.DontRequireReceiver);
				break;
			case ParameterType.GameObject:
				_sender.SendMessage (_listener.sendMessage,_listener.gameObjectParameter, SendMessageOptions.DontRequireReceiver);
				break;
			case ParameterType.Int:
				_sender.SendMessage (_listener.sendMessage,_listener.intParameter, SendMessageOptions.DontRequireReceiver);
				break;
			case ParameterType.String:
				_sender.SendMessage (_listener.sendMessage,_listener.stringParameter, SendMessageOptions.DontRequireReceiver);
				break;
			}
		}


	}
}

