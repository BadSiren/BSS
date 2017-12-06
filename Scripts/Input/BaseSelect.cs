using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using BSS.Unit;

namespace BSS {
	public class BaseSelect : SerializedMonoBehaviour
	{
		public Texture2D selectCircle;
		public static BaseSelect instance;
		public enum ESelectState
		{
			None,AllySelect,EnemySelect,MultiSelect
		}
		public ESelectState eSelectState;
		public List<GameObject> selectObjects=new List<GameObject> ();


		void Awake() {
			instance = this;
		}

		public void allyUnitSelect(GameObject obj) {
			selectObjects.Clear ();
			selectObjects.Add(obj);
			eSelectState = ESelectState.AllySelect;
			BaseEventListener.onPublishGameObject ("UnitSelect", obj);
			BaseEventListener.onPublishGameObject ("AllyUnitSelect", obj);
		}
		public void enemyUnitSelect(GameObject obj) {
			selectObjects.Clear ();
			selectObjects.Add(obj);
			eSelectState = ESelectState.EnemySelect;
			BaseEventListener.onPublishGameObject ("UnitSelect", obj);
			BaseEventListener.onPublishGameObject ("EnemyUnitSelect", obj);
		}
		public void multiUnitSelect(List<GameObject> objs) {
			selectObjects.Clear ();
			selectObjects = objs;
			eSelectState = ESelectState.MultiSelect;
			BaseEventListener.onPublish ("SelectCancle");
		}
		public void unitUnSelect(GameObject obj) {
			if (!selectObjects.Contains(obj)) {
				return;
			}
			selectObjects.Remove (obj);
			if (selectObjects.Count == 0) {
				selectCancle ();
			} else if (selectObjects.Count == 1) {
				allyUnitSelect (selectObjects [0]);
			}
		}
		public void selectCancle() {
			eSelectState = ESelectState.None;
			selectObjects.Clear ();
			BaseEventListener.onPublish ("SelectCancle");
		}

		void OnGUI() {
			switch (eSelectState) {
			case ESelectState.AllySelect:
			case ESelectState.EnemySelect:
			case ESelectState.MultiSelect:
				for (int i = 0; i < selectObjects.Count; i++) {
					Vector3 screenPoint = Camera.main.WorldToScreenPoint (new Vector3 (selectObjects [i].transform.position.x, -selectObjects [i].transform.position.y, 0f));
					GUI.DrawTexture (new Rect (screenPoint.x - 30f, screenPoint.y - 20f, 60f, 40f), selectCircle, ScaleMode.ScaleToFit);
				}
				break;
			}
		}


	}
}

