using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.UI {
	public class UnitInfo : UnitBoard
	{
		//TextExt.requestVariable -> Board.request -> TextExt.responceVariable
		public override void setSelectUnit(BaseUnit unit) {
			base.setSelectUnit (unit);
			sendToReceiver ("Team", teamToString(unit.team.ToString()));
			sendToReceiver ("Team", teamToColor(unit.team.ToString()));
			sendToReceiver ("UName", unit.uName);
			if (unit.isInvincible) {
				sendToReceiver ("Health", "-공격불가-");
			} else {
				sendToReceiver ("Health", unit.health.ToString ("0") + "/" + unit.maxHealth.ToString ("0"));
			}
			sendToReceiver ("Mana", unit.mana.ToString ("0") + "/" + unit.maxMana.ToString ("0"));
			sendToReceiver ("Portrait", unit.portrait);

			sendToReceiver ("Armor", valueToString(unit.initArmor,unit.armor));

			Attackable attackable = unit.GetComponent<Attackable> ();
			if (attackable == null) {
				sendBoolToReceiver ("Attack", false);
				sendBoolToReceiver ("AttackIcon", false);
				sendBoolToReceiver ("ShortAttack", false);
				sendBoolToReceiver ("LongAttack", false);
				sendBoolToReceiver ("AttackUp", false);
			} else {
				sendToReceiver ("Attack", valueToString(attackable.initDamage,attackable.damage));
				sendBoolToReceiver ("Attack", true);
				sendToReceiver ("AttackIcon", attackable.icon);
				sendBoolToReceiver ("AttackUp", false);
			}
		}

		public void setSelectUnit(GameObject obj) {
			var _unit=obj.GetComponent<BaseUnit> ();
			if (_unit == null) {
				return;
			}
			setSelectUnit (_unit);
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
			UIController.instance.informBoard.Show (selectUnit.uName + " 공격정보",text1+"\n"+text2+"\n"+text3);
		}
		public void buttonDefenceInfo() {
			if (selectUnit == null) {
				return;
			}
			string text0 = "방어력: " + valueToString (selectUnit.initArmor, selectUnit.armor);
			string text1 = "피해감소율: " + valueToStringFloor (BaseUnit.reductionArmor(selectUnit.initArmor)*100f, BaseUnit.reductionArmor(selectUnit.armor)*100f) +"%";
			UIController.instance.informBoard.Show (selectUnit.uName + " 방어정보",text0+"\n"+text1);
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
		private string teamToString(string team) {
			switch (team) {
			case "Red":
				return "아군";
			case "Blue":
				return "적군";
			case "White":
				return "중립";
			}
			return "";
		}
		private Color teamToColor(string team) {
			switch (team) {
			case "Red":
				return Color.green;
			case "Blue":
				return Color.red;
			case "White":
				return Color.white;
			}
			return Color.white;
		}
		private string typeToString(string _type) {
			switch (_type) {
			case "Short":
				return "근접 공격";
			case "Long":
				return "원거리 공격";
			}
			return "";
		}
	}
}

