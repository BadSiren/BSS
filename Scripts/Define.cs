using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS {
	public enum ESelectState {
		None,Mine,NotMine,Multi,
		All
	}
	public struct ArgWithRecevier
	{
		public string receiverName;
		public object parameter;
	}

	public class Define : MonoBehaviour 
	{

	}
}

