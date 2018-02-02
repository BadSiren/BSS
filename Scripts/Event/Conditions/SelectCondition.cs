using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using BSS.Unit;


namespace BSS.Event {
    public class SelectCondition : MonoBehaviour
	{
        [FoldoutGroup("State")]
		public ESelectState selectState;

		public bool selectStateEqual () {
            return BaseSelect.instance.eSelectState==selectState;
		}
	}
}

