using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSS.UI {
	public class EnterSelect : PageBoard {

		public int mode=0;

		public override void Show() {
			base.Show ();
			pageViewUpdate ();
		}

		public override void pageViewUpdate() {
			var levelInfo=BSDatabase.instance.levelDatabase.levelInfos [mode.ToString () + "/" + page.ToString ()];

			sendToReceiver ("Title", page.ToString()+"단계 ["+levelInfo.title+"]");
			sendToReceiver ("MaxLevel", levelInfo.maxLevel.ToString());
			sendToReceiver ("ClearMoney", levelInfo.clearMoney.ToString());

			sendToReceiver ("Page", page.ToString ());
		}
		public void enterGame() {
			if (validate ()) {
				LoadBase.instance.selcectMode = 0;
				LoadBase.instance.selcectLevel = page;
				LoadBase.instance.loadPlayScene ();
			} else {
				Close ();
				BaseEventListener.onPublish ("NoBuyUnit");
			}
		}

		private bool validate() {
			string container = "UnitShop";
			if (UserJson.instance.getEmptySpace (container)==UserJson.instance.containerMax[container]) {
				return false;
			}
			return true;
		}
		/*
		public enum EnterState
		{
			ModeSelect,LevelSelect
		}
		[System.Serializable]
		public struct ButtonData
		{
			public string _text;
			public Sprite _image;
		}
		public EnterState enterState;

		public int page;
		public string modeTitle;
		public string levelTitle;
		public List<ButtonData> modeButtons=new List<ButtonData>();
		public List<ButtonData> levelButtons=new List<ButtonData>();

		void Start() {
		}

		public override void Show() {
			base.Show();
			enterState = EnterState.ModeSelect;
			for (int i = 0; i < 3; i++) {
				sendBoolToReceiver ("Button" + i.ToString (), false);
				sendBoolToReceiver ("Icon" + i.ToString (), false);
			}
				
			sendToReceiver ("Title", modeTitle);
			for (int i = 0; i < modeButtons.Count; i++) {
				sendBoolToReceiver ("Button" + i.ToString (), true);
				sendToReceiver ("Button" + i.ToString (), modeButtons [i]._text);
				sendToReceiver ("Icon" + i.ToString (), modeButtons [i]._image);
			}

		}
		public void LevelShow() {
			base.Show();
			enterState = EnterState.LevelSelect;
			for (int i = 0; i < 3; i++) {
				sendBoolToReceiver ("Button" + i.ToString (), false);
				sendBoolToReceiver ("Icon" + i.ToString (), false);
			}

			sendToReceiver ("Title", levelTitle);
			for (int i = 0; i < levelButtons.Count; i++) {
				sendBoolToReceiver ("Button" + i.ToString (),true);
				sendToReceiver ("Button" + i.ToString (), levelButtons [i]._text);
				sendToReceiver ("Icon" + i.ToString (), levelButtons [i]._image);
			}
			return;
		}

		public void SelectButton(int num) {
			if (enterState == EnterState.ModeSelect) {
				LoadBase.instance.selcectMode = num;
				LevelShow ();
				return;
			}
			LoadBase.instance.selcectLevel = num;
			LoadBase.instance.loadPlayScene ();
		}
		*/
	}
}