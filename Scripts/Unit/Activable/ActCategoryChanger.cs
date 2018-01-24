using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BSS.UI;

namespace BSS.Unit {
	public class ActCategoryChanger : Activable
	{
		public string changeCategory;

		public override void initialize ()
		{
		}
		public override void activate() {
            activables.setCategory(changeCategory);
		}

	}
}


