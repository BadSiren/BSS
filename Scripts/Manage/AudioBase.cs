using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.AudioBase {
	public class AudioBase : SerializedMonoBehaviour
	{
		public Dictionary<string,AudioSource> sources=new Dictionary<string,AudioSource>();


		public void playOnce(string sourceID) {
			if (sources.ContainsKey (sourceID)) {
				sources [sourceID].PlayOneShot (sources [sourceID].clip);
			}
		}
	}
}

