using UnityEngine;
using System.Collections;

namespace BSS {
	public interface IItemPropertyApply 
	{
		void addProperty(string ID,float value);
		void removeProperty(string ID,float value);
	}
}

