using UnityEngine;
using System.Collections;
using EventsPlus;

namespace BSS {
	[RequireComponent(typeof(Collider2D))]
	public class Clickable : MonoBehaviour
	{
		public Vector3 lastedMousePoint;
		public int priority;
		public PublisherVector3 pClick;
		public PublisherVector3 pDobuleClick;
		public PublisherVector3 pLongClick;

		void Awake() {
			pClick.initialize();
			pDobuleClick.initialize();
			pLongClick.initialize();
		}
		void OnDestroy() {
			pClick.clear ();
			pDobuleClick.clear ();
			pLongClick.clear ();
		}

		public virtual void onClick(Vector3 mousePos) {
			lastedMousePoint = mousePos;
			pClick.publish (mousePos);
		}
		public virtual void onDoubleClick(Vector3 mousePos) {
			lastedMousePoint = mousePos;
			pDobuleClick.publish (mousePos);
		}
		public virtual void onLongClick(Vector3 mousePos) {
			lastedMousePoint = mousePos;
			pLongClick.publish (mousePos);
		}
	}
}

