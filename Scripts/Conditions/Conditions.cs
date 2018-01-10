using UnityEngine;
using System.Collections;
using BSS.Input;

namespace BSS {
	public enum Condition {
		MineSelect,IsSelected
	}
	public class Conditions : MonoBehaviour
	{
		public static Conditions instance;

		void Awake() {
			instance = this;
		}
		public bool validate(Condition condition,GameObject clickObj) {
			switch (condition) {
			case Condition.MineSelect:
				return mineSelectValidate();
			case Condition.IsSelected:
				return isSelectedValidate(clickObj);
			}

			return false;
		}

		private bool mineSelectValidate() {
			return (BaseSelect.instance.eSelectState == ESelectState.Mine);
		}
		private bool isSelectedValidate(GameObject clickObj) {
			var selectable=clickObj.GetComponent<Selectable> ();
			if (selectable == null) {
				return false;
			}
			return (BaseSelect.instance.selectableList.Contains(selectable));
		}
	}
}

