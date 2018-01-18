using UnityEngine;
using System.Collections;

namespace BSS {
	public class EffectCreator : MonoBehaviour
	{
		public GameObject effectParticle;

		public void effectCreate(GameObject obj) {
			GameObject.Instantiate (effectParticle,obj.transform.position,Quaternion.identity);
		}
		public void effectCreate(Vector2 pos) {
			GameObject.Instantiate (effectParticle,pos,Quaternion.identity);
		}
	}
}

