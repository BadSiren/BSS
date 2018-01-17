using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS {
	public class DestroyEventer : MonoBehaviour
	{
		public bool isParent;
		[ShowIf("isParent")]
		public GameObject parent;
		public string gameObjectEvent="";
		public string instanceIDEvent="";

		void OnDestroy() {
			GameObject obj = gameObject;
			if (isParent && parent != null) {
				obj = parent;
			}
			BaseEventListener.onPublishGameObject (gameObjectEvent, obj, obj);
			BaseEventListener.onPublishInt (gameObjectEvent, obj.GetInstanceID(), obj);
		}
	}
}

