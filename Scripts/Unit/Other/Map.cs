using UnityEngine;
using System.Collections;
using BSS.Unit;
using BSS.Input;

namespace BSS {
	public class Map : MonoBehaviour
	{
		public void mapClick() {
			BaseEventListener.onPublish ("MapClick");
		}
		public void mapDoubleClick() {
			BaseEventListener.onPublish ("MapDoubleClick");
			selectedToMoveByForce ();
		}

		public void selectedToMoveByForce() {
			var baseSelect = BaseSelect.instance;
			if (baseSelect.eSelectState != BaseSelect.ESelectState.AllySelect && baseSelect.eSelectState != BaseSelect.ESelectState.MultiSelect) {
				return;
			}
			Vector3 mousePoint = BaseInput.getMousePoint2D ();
			foreach (var it in BaseSelect.instance.selectObjects) {
				it.SendMessage ("toMoveByForce", mousePoint, SendMessageOptions.DontRequireReceiver);
			}
		}


	}
}

