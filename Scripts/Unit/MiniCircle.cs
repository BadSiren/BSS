using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    [RequireComponent(typeof(SpriteRenderer))]
    public class MiniCircle : MonoBehaviour {
        private SpriteRenderer spriteRenderer;
        private BaseUnit unit;

        void Start() {
            unit=GetComponentInParent<BaseUnit>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (unit==null || !unit.onlyMine) {
                spriteRenderer.enabled = false;
                Destroy(this);
                return;
            }
            spriteRenderer.enabled = true;
        }
    }
}
