using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.Unit;

namespace BSS {
	public struct ArgWithRecevier
	{
		public string receiverName;
		public object parameter;
	}
	public struct MessageArgsTwo
	{
		public object parameter0;
		public object parameter1;
	}
	public class MessageCallback
	{
		public string EventType;
		public object callback;
	}


	public class Define : MonoBehaviour 
	{

	}
}

