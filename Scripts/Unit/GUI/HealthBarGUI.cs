using UnityEngine;
using System.Collections;

namespace BSS.Unit {
	public class HealthBarGUI : MonoBehaviour
	{
		[HideInInspector]
		public float alpha;

		public bool displayValue = true;
		public PositionModes positionMode;
		public enum PositionModes {
			Fixed,
			Center
		}
			/// <summary>
			/// Put here white texture.
			/// </summary>
		public Texture texture;
		public Color addedHealth = Color.green;
		public Color availableHealth = Color.white;
		public Color displayedValue = Color.white;
		public Color drainedHealth = Color.red;
		public float animationSpeed = 3f;
		public float transitionSpeed = 2f;
		public float transitionDelay = 3f;
		public float visibility = 1;
		public int width = 120;
		public int height = 16;
		public Vector2 offset = new Vector2(0, 0);
		public Vector2 valueOffset = new Vector2(0, -46);
		public GUIStyle textStyle;
	}
}	

