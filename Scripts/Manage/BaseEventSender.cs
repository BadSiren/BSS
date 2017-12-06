using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS {
	public class BaseEventSender : SerializedMonoBehaviour
	{
		public enum ParameterType
		{
			Void
		}
		public string sendName;
		public ParameterType sendType;

		public void onPublish() {
			switch (sendType) {
			case ParameterType.Void:
				BaseEventListener.onPublish (sendName);
				break;
			}
		}

	}
}

