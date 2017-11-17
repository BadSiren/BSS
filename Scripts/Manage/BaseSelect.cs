using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;
using BSS.UI;

namespace BSS {
	public class BaseSelect : MonoBehaviour
	{
		public static BaseSelect instance;

		public GameObject lastedSelect;
		public bool isBaseSelect=true;

		public void Awake()
		{
			instance = this;
		}
		public void setSelect(GameObject obj) {
			BaseUnit unit=obj.GetComponent<BaseUnit> ();
			if (isBaseSelect && unit!=null) {
				UIController.instance.selectUnitEvent (unit);
			}

		}
	}
}



