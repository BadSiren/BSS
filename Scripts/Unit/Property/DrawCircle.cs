using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	[RequireComponent (typeof (BaseUnit))]
	public class DrawCircle : MonoBehaviour
	{
		public Sprite circleSpr;
		public Vector3 scale=new Vector3(0.5f,0.5f,1f);

		private SpriteRenderer render;

		void Awake() {
			var obj = new GameObject ("DrawCircle");
			obj.transform.SetParent (this.transform,false);
			obj.transform.localScale = scale;
			render=obj.AddComponent<SpriteRenderer> ();
			render.sprite = circleSpr;
			drawCircleOff ();
		}
		public void drawCircleOn() {
			render.enabled = true;
		}
		public void drawCircleOff() {
			render.enabled = false;
		}
		public void drawCircleToggle() {
			render.enabled = !render.enabled;
		}
		//UnitEvent
		private void onSelectEvent() {
			drawCircleOn ();
		}
		private void onUnSelectEvent() {
			drawCircleOff ();
		}

	}
}

