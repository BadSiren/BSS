using UnityEngine;
using System.Collections;

namespace BSS.LobbyItemSystem {
	public abstract class LobbyAct : ScriptableObject
	{
		public abstract void activate (int num,string containerName);
	}
}

