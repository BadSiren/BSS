using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS.UI {

	public class UIController : MonoBehaviour
	{
		public static UIController instance;
		public InformBoard informBoard;
		public UnitInfo unitInfo;
		public ActiveBoard activeBoard;

		void Awake()
		{
			instance = this;
			if (informBoard == null || unitInfo==null || activeBoard==null) {
				Debug.LogError ("Board is null");
			}
		}

	}
}

