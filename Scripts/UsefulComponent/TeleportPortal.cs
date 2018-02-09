using UnityEngine;
using System.Collections;
using BSS.Unit;
using BSS.Event;

namespace BSS {
    [RequireComponent(typeof(Collider2D))]
    public class TeleportPortal : MonoBehaviour
    {
        public GameObject exit;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var unit= collision.gameObject.GetComponentInParent<BaseUnit>();
            if (unit == null || !unit.onlyMine) {
                return;
            }
            unit.transform.position = exit.transform.position;
            UnitControl.ToStop(unit);
        }
    }
}
