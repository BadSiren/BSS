using UnityEngine;
using System.Collections;

namespace BSS {
	public interface IItemPropertyApply 
	{
		void applyProperty(string ID,float value);
		void cancleProperty(string ID,float value);
	}
}

