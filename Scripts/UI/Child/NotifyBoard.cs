using UnityEngine;
using System.Collections;

namespace BSS.UI {
	public class NotifyBoard : Board
	{
		public float time=2f;
		public override void Show() {
			base.Show ();

			sendBoolToReceiver ("Title", false);
			sendBoolToReceiver ("Text", false);
			StopAllCoroutines ();
			StartCoroutine (setClose (time));

		}
		public void Show(string _title,string _text) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendToReceiver ("Text", _text);
			StopAllCoroutines ();
			StartCoroutine (setClose (time));
		}
		public void Show(string _title,string _text,float _time) {
			base.Show ();

			sendToReceiver ("Title", _title);
			sendToReceiver ("Text", _text);
			StopAllCoroutines ();
			StartCoroutine (setClose (_time));
		}

		IEnumerator setClose(float _time) {
			int i = 0;
			while (i<50) {
				yield return new WaitForSeconds (_time/50);
				canvasGroup.alpha -= 0.01f;
				i++;
			}
			Close ();
		}
	}
}

