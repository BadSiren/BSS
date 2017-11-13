using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventsPlus;

namespace BSS.UI {
	[RequireComponent (typeof(CanvasGroup))]
	public class Board : MonoBehaviour
	{
		public string boardName;
		public bool isStartedShow;
		public static List<Board> boardList = new List<Board>();

		protected CanvasGroup canvasGroup;
		private ArgWithRecevier messageArgs = new ArgWithRecevier();

		public void Awake()
		{
			canvasGroup=GetComponent<CanvasGroup> ();


			initailze ();
		}
		protected virtual void initailze() {
			boardList.Add (this);

			if (isStartedShow) {
				Show ();
			} else {
				Close ();
			}
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

			BroadcastMessage ("BoardShowEvent", SendMessageOptions.DontRequireReceiver);
		}
		public virtual void Close() {
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}
		public static Board find(string _name) {
			Board temp=boardList.Find (x => x.boardName == _name);
			return temp;
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

			
	}
}

