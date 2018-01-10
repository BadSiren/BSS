using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace BSS.UI {
	[RequireComponent (typeof(CanvasGroup))]
	public class Board : SerializedMonoBehaviour
	{
		public string boardName;
		public bool isStartedShow;
		public static List<Board> boardList = new List<Board>();
		[FoldoutGroup("Event")]
		public UnityEvent onShow = new UnityEvent();
		[FoldoutGroup("Event")]
		public UnityEvent onClose = new UnityEvent();

		protected CanvasGroup canvasGroup;
		private ArgWithRecevier messageArgs = new ArgWithRecevier();


		public void Awake()
		{
			canvasGroup=GetComponent<CanvasGroup> ();

			initialize ();
		}
		void Start() {
			if (isStartedShow) {
				Show ();
			} 
		}
		protected virtual void initialize() {
			boardList.Add (this);
		}
		public void OnDestroy()
		{
			deInitailze ();
		}
		protected virtual void deInitailze() {
			boardList.Remove (this);
		}

		public virtual void Show() {
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;

			onShow.Invoke ();
		}
		public virtual void Close() {
			if (canvasGroup.alpha == 0f) {
				return;
			}
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;

			onClose.Invoke ();
		}

		public void sendBoolToReceiver(string receiverName,bool _enabled) {
			messageArgs.receiverName = receiverName;
			messageArgs.parameter = _enabled;
			BroadcastMessage ("receiveBoolMessageEvent", messageArgs, SendMessageOptions.DontRequireReceiver);
		}
		public void sendToReceiver(string receiverName,string _text) {
			messageArgs.receiverName = receiverName;
			messageArgs.parameter = _text;
			BroadcastMessage ("receiveMessageEvent", messageArgs, SendMessageOptions.DontRequireReceiver);
		}
		public void sendToReceiver(string receiverName,Sprite _spr) {
			messageArgs.receiverName = receiverName;
			messageArgs.parameter = _spr;
			BroadcastMessage ("receiveMessageEvent", messageArgs, SendMessageOptions.DontRequireReceiver);
		}
		public void sendToReceiver(string receiverName,Color _color) {
			messageArgs.receiverName = receiverName;
			messageArgs.parameter = _color;
			BroadcastMessage ("receiveMessageEvent", messageArgs, SendMessageOptions.DontRequireReceiver);
		}
		public void sendIndexToReceiver(string receiverName,int _index) {
			messageArgs.receiverName = receiverName;
			messageArgs.parameter = _index;
			BroadcastMessage ("receiveIndexMessageEvent", messageArgs, SendMessageOptions.DontRequireReceiver);
		}

			
	}
}

