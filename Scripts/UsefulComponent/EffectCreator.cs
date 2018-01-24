using UnityEngine;
using System.Collections;

namespace BSS {
	public class EffectCreator : MonoBehaviour
	{
		public GameObject effectParticle;

		public void effectCreate(GameObject obj) {
		    Instantiate (effectParticle,obj.transform.position,Quaternion.identity);
		}
		public void effectCreate(Vector2 pos) {
			Instantiate (effectParticle,pos,Quaternion.identity);
		}
	}
}

