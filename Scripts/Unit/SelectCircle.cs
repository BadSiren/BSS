using UnityEngine;
using System.Collections;

namespace BSS.Unit {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SelectCircle : MonoBehaviour
    {
        public Sprite mineSprite;
        public Sprite notMineSprite;
        private SpriteRenderer spriteRenderer;
        private Selectable selectable;

        void Start() {
            spriteRenderer=GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            selectable = GetComponentInParent<Selectable>();
            if (selectable == null) {
                Destroy(gameObject);
                return;
            }
            if (selectable.owner.onlyMine) {
                spriteRenderer.sprite = mineSprite;
            } else {
                spriteRenderer.sprite = notMineSprite;
            }
        }

        public void updateRender() {
            spriteRenderer.enabled = selectable.isSelect;
        }
    }
}
