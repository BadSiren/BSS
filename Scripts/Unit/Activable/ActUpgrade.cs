using UnityEngine;
using System.Collections;
using BSS.Data;

namespace BSS.Unit {
	[System.Serializable]
	public class ActUpgrade: Activable
	{
		public string upgradeIndex;
		public int useInitMoney;
		public int useAddMoney;
		public int useInitFood;
		public int useAddFood;

		private int level=0;

		private MessageCallback callback=new MessageCallback();


		public override void activate(BaseUnit selectUnit) {
			updateLevel ();
			upgrade ();
			showInformDynamic ();
		}

		public override void activateLongPress(BaseUnit selectUnit) {
			updateLevel ();
			base.activateLongPress (selectUnit);
		}

		private void upgrade() {
			if (GameDataBase.instance.useMoneyFood(getMoney(),getFood())) {
				GameDataBase.instance.SendMessage ("increase" + upgradeIndex+"Event", SendMessageOptions.DontRequireReceiver);
				updateLevel ();
			}
		}
		private void updateLevel() {
			GameDataBase.instance.SendMessage ("get" + upgradeIndex+"Event", callback, SendMessageOptions.DontRequireReceiver);
			level = (int)callback.callback;
			string _level = "@";
			_level=System.Text.RegularExpressions.Regex.Replace(textContent, "[^0-9]+", string.Empty);
			textContent=textContent.Replace(_level,level.ToString());
		}

		protected override int getMoney() {
			return useInitMoney+level*useAddMoney;
		}
		protected override int getFood() {
			return useInitFood+level*useAddFood;
		}

	}
}

