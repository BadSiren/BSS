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
		public bool isAttack=true;

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
			if ((eSelectState==ESelectState.AllySelect ||eSelectState==ESelectState.MultiSelect) && isAttack) {
				foreach (var it in selectObjects) {
					var attakable = it.GetComponent<Attackable> ();
					if (attakable == null) {
						continue;
					}
					var movable=it.GetComponent<Movable> ();
					if (movable != null) {
						movable.toMoveTarget (obj, attakable.range);
					}
				}
				return;
			}
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


	}
}

