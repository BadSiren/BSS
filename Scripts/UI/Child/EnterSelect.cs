using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS.UI {
	public class EnterSelect : PageBoard {
        public class LevelData {
            public Dictionary<string, string> elementDics;
            public Dictionary<string, Sprite> sprites;
        }
        public List<LevelData> levelDatas = new List<LevelData>();

		public override void Show() {
			base.Show ();
			pageUpdate ();
		}

		public override void pageUpdate() {
            base.pageUpdate();
            var levelData = levelDatas[page];
            foreach (var it in levelData.elementDics) {
                if (it.Key.Contains("Spr")) {
                    sendToReceiver(it.Key, levelData.sprites[it.Value]);
                } else {
                    sendToReceiver(it.Key, it.Value);
                }
            }
		}
	}
}