using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BSS.UI {
	public class ElementBase: MonoBehaviour
	{
		public enum ContentType
		{
			Text,Image,Button,Toggle
		}
			
		public ContentType contentType;
		public string elementName;

		protected Board board;
		protected Text textComp;
		protected Image imageComp;
		protected Button buttonComp;
		protected Toggle toggleComp;


		void Awake() {
			switch (contentType) {
			case ContentType.Text:
				textComp = GetComponent<Text> ();
				break;
			case ContentType.Image:
				imageComp = GetComponent<Image> ();
				break;
			case ContentType.Button:
				buttonComp = GetComponent<Button> ();
				break;
			case ContentType.Toggle:
				toggleComp = GetComponent<Toggle> ();
				break;
			}

			board = transform.GetComponentInParent<Board> ();
			if (board==null) {
				Debug.LogError ("Require Board Class in Parent");
			}
		}
		public void elementEnabled(bool _enabled) {
			switch (contentType) {
			case ContentType.Text:
				textComp.enabled = _enabled;
				break;
			case ContentType.Image:
				imageComp.enabled = _enabled;
				break;
			case ContentType.Button:
				buttonComp.enabled = _enabled;
				break;
			case ContentType.Toggle:
				toggleComp.enabled = _enabled;
				break;
			}
		}

		private void receiveMessageEvent(ArgWithRecevier _messageArgs){
			if (_messageArgs.receiverName != elementName) {return;}
			string _text = _messageArgs.parameter as string;
			if (_text == null) {
				Sprite spr = _messageArgs.parameter as Sprite;
				if (spr == null) {
					if (_messageArgs.parameter is Color) {
						Color col = (Color)_messageArgs.parameter;
						elementEnabled (true);
						updateVariable (col);
					}
					return;
				}
				elementEnabled (true);
				updateVariable (spr);
				return;
			} 
			elementEnabled (true);
			updateVariable (_text);
		}
		private void receiveBoolMessageEvent(ArgWithRecevier _messageArgs){
			if (_messageArgs.receiverName!="All" &&_messageArgs.receiverName != elementName) {return;}
			bool _enabled = (bool)_messageArgs.parameter;
			elementEnabled (_enabled);
		}



		public void updateVariable(Sprite changeSprite) {
			if (contentType != ContentType.Image) {
				return;
			}
			imageComp.sprite = changeSprite;
		}
		public void updateVariable(Color _color) {
			if (contentType == ContentType.Text) {
				textComp.color = _color;
				return;
			}
			if (contentType == ContentType.Image) {
				imageComp.color = _color;
				return;
			}
		}
		public void updateVariable(string resultText) {
			if (contentType != ContentType.Text) {
				return;
			}
			textComp.text = resultText;
		}

		public void updateVariable(int resultText) {
			updateVariable (resultText.ToString ());
		}
		public void updateVariable(float resultText) {
			updateVariable (resultText.ToString ());
		}



		public virtual void BoardShowEvent() {}
	}

}