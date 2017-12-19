using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BSS {
	public class ChangeButton : MonoBehaviour, IPointerUpHandler
	{
		public bool isSelect;
		public UnityEvent noSelectClick = new UnityEvent();
		public UnityEvent selectClick = new UnityEvent();

		public void OnPointerUp(PointerEventData eventData)
		{
			if (isSelect) {
				isSelect = false;
				selectClick.Invoke ();
			} else {
				isSelect = true;
				noSelectClick.Invoke ();
			}
		}
	}
}

