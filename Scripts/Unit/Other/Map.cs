using UnityEngine;
using System.Collections;
using BSS.Unit;
using BSS.Input;
using Photon;

namespace BSS {
	public class Map : Photon.MonoBehaviour
	{


		public void toMoveByForceInMousePoint() {
			Vector2 mousePoint = BaseInput.getMousePoint ();
			foreach (var it in BaseSelect.instance.selectableList) {
				UnitUtils.ToMove (it.owner, mousePoint);
			}
		}


	}
}

