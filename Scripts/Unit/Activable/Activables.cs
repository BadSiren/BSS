using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
	public class ActivableInfo {
		public Sprite icon;
		public string titleContent;
		[TextArea()]
		public string textContent;
	}
	public class Activables : SerializedMonoBehaviour
	{
		public Dictionary<string,ActivableInfo> database;
	}
}

