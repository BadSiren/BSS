using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;
using BSS.Play;

namespace BSS.LobbyItemSystem {
	public class AddMoney : EquipProperty  {
		/*
		[TextArea()]
		[Header("AddMoney : Optional")]
		[Header("StartMoney : Optional")]
		public string description="";

		private int startMoney;
		private int addMoney;

		public override void initialize (Dictionary<string,string> args)
		{
			if (args.ContainsKey ("StartMoney")) {
				startMoney = int.Parse (args ["StartMoney"]);
			}
			if (args.ContainsKey ("AddMoney")) {
				addMoney = int.Parse(args ["AddMoney"]);
			}
		}
		public override void onGameStartAct() {
			GameDataBase.instance.money += startMoney;
		}
		public override void onLevelClearAct() {
			GameDataBase.instance.money += addMoney;
		}
		public override string getDescription(Dictionary<string,string> args) {
			string revise = description;
			string nextString = "0";
			if (args.ContainsKey ("StartMoney")) {
				nextString = args ["StartMoney"];
			} 
			revise = revise.Replace ("@StartMoney", nextString);
			nextString = "0";
			if (args.ContainsKey ("AddMoney")) {
				nextString = args ["AddMoney"];
			}
			revise = revise.Replace ("@AddMoney", nextString);
			return revise;
		}
		*/
	}
}

