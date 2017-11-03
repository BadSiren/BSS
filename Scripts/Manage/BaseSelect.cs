using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS {
	public class BaseSelect : MonoBehaviour
	{
		public static BaseSelect instance;

		public GameObject lastedSelect;
		public PublisherGameObject pSelectEvent;

		public void Awake()
		{
			instance = this;

			pSelectEvent.initialize();
		}
		public void OnDestroy()
		{
			pSelectEvent.clear();
		}
		public void setSelect(GameObject obj) {
			pSelectEvent.publish (obj);
		}
	}
}



