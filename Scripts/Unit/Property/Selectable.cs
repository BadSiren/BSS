using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using BSS.Unit;
using Sirenix.OdinInspector;

namespace BSS {
	[RequireComponent(typeof(BaseUnit))]
	public class Selectable : MonoBehaviour
	{
        public static List<Selectable> selectableList = new List<Selectable>();
		

		[HideInInspector]
		public BaseUnit owner;
		public bool isSelected {
			get {
				return BaseSelect.instance.selectableList.Contains (this);
			}
		}

		public void Awake()
		{
			owner = GetComponent<BaseUnit> ();
			selectableList.Add(this);
		}
		void OnDestroy()
		{
            onDeselect();
			selectableList.Remove(this);
		}

		public void onSelect() {
			BaseSelect.instance.unitSelect (this);
		}
		public void onDeselect() {
			BaseSelect.instance.selectRemove (this);
		}
	}
}

