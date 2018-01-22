using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.UI {
	public class UnitInfo : UnitBoard
	{

		public override void changeSelectUnit (BaseUnit unit)
		{
			selectUnit = unit;
			updateInfomation (unit);
		}
		public override void clearSelectUnit ()
		{
			sendBoolToReceiver ("All", false);
		}

		private void updateInfomation(BaseUnit unit) {
			if (unit.isSceneObject) {
				sendToReceiver ("Team", "중립");
				sendToReceiver ("Team", Color.white);
			} else {
				sendToReceiver ("Team", "Player" + unit.photonView.ownerId.ToString ());
				sendToReceiver ("Team", teamToColor (unit.photonView.ownerId));
			}
			sendToReceiver ("UName", unit.uName);
			if (unit.isInvincible) {
				sendToReceiver ("Health", "-공격불가-");
				sendBoolToReceiver ("MaxHealth",false);
			} else {
				sendToReceiver ("Health", unit.health.ToString ("0") );
				sendToReceiver ("MaxHealth", unit.maxHealth.ToString ("0"));
			}
            sendToReceiver ("Mana", unit.mana.ToString ("0") );
            sendToReceiver ("MaxMana", unit.maxMana.ToString ("0"));
			sendToReceiver ("Portrait", unit.portrait);

			sendToReceiver ("Armor", valueToString(unit.initArmor,unit.armor));
			sendBoolToReceiver ("ArmorIcon", true);

			Attackable attackable = unit.GetComponent<Attackable> ();
			if (attackable == null) {
				sendBoolToReceiver ("Attack", false);
				sendBoolToReceiver ("AttackIcon", false);
			} else {
				sendToReceiver ("Attack", valueToString(attackable.initDamage,attackable.damage));
				sendBoolToReceiver ("Attack", true);
				sendToReceiver ("AttackIcon", attackable.icon);
			}
		}

		public void lookAtUnit() {
			if (selectUnit == null) {
				return;
			}
			Camera.main.transform.position = selectUnit.transform.position;
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -10f);
		}

		public void buttonAttackInfo() {
			if (selectUnit == null) {
				return;
			}
			Attackable attackable=selectUnit.GetComponent<Attackable> ();
			//string text0 = "공격형태: " + typeToString (attackable.attackType.ToString());
			string text1 = "공격력: " + valueToString (attackable.initDamage, attackable.damage);
			string text2 = "공격속도: " + valueToString (attackable.initAttackSpeed, attackable.attackSpeed);
			string text3 = "사거리: " + valueToString (attackable.initRange, attackable.range);
			(Board.boardList.Find (x => x is InformBoard) as InformBoard).Show (selectUnit.uName + " 공격정보",text1+"\n"+text2+"\n"+text3);
		}
		public void buttonDefenceInfo() {
			if (selectUnit == null) {
				return;
			}
			string text0 = "방어력: " + valueToString (selectUnit.initArmor, selectUnit.armor);
            string text1 = "피해감소율: " + valueToStringFloor (UnitUtils.GetDamageReduction(selectUnit.initArmor)*100f, UnitUtils.GetDamageReduction(selectUnit.armor)*100f) +"%";
			string text2 = "";
			var movable=selectUnit.GetComponent<Movable> ();
			if (movable!=null){
				text2="이동속도 : " + valueToString (movable.initSpeed, movable.speed);
			}
			(Board.boardList.Find (x => x is InformBoard) as InformBoard).Show (selectUnit.uName + " 방어정보",text0+"\n"+text1+"\n"+text2);
		}


		private string valueToString(float initValue,float allValue) {
			//Ex) initValue=2000,allValue=2500 => 2000 (+500)
			string initText = string.Format ("{0:0.##}", initValue);
			string addText = string.Format ("{0:0.##}", allValue - initValue);
			return initText +" (+"+ addText+")";
		}
		private string valueToStringFloor(float initValue,float allValue) {
			//Ex) initValue=2000,allValue=2500 => 2000 (+500)
			string initText = string.Format ("{0:0}", initValue);
			string addText = string.Format ("{0:0}", allValue - initValue);
			return initText +" (+"+ addText+")";
		}

		private Color teamToColor(int playerID) {
			switch (playerID) {
			case 1:
				return Color.red;
			case 2:
				return Color.blue;
			case 3:
				return Color.green;
			}
			return Color.white;
		}
	}
}

