using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventsPlus;
using BSS.Unit;

namespace BSS {
	[System.Serializable]
	public class PublisherInt : Publisher<int>
	{
	}
	[System.Serializable]
	public class PublisherVector3 : Publisher<Vector3>
	{
	}
	[System.Serializable]
	public class PublisherGameObject : Publisher<GameObject>
	{
	}
	[System.Serializable]
	public class PublisherBaseUnit : Publisher<BaseUnit>
	{
	}
	[System.Serializable]
	public class PublisherAttackInfo : Publisher<AttackInfo>
	{
	}

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


	//SendMessage =>This gameObject is all class.
	//Publisher =>All gameObject is static class.


	public class Define : MonoBehaviour 
	{

	}
}

