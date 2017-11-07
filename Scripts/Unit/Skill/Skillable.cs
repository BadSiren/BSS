using UnityEngine;
using System.Collections;

namespace BSS.Skill {
	[System.Serializable]
	public class Skillable 
	{
		public SkillBase skillBase;
		public int level;

		private SkillBase preSkillBase;

		public void addComponent(GameObject obj) {
			if (skillBase == null) {
				return;
			}
			GameObject.Destroy(preSkillBase);

			preSkillBase = skillBase;
			preSkillBase.skillable = this;
			copyComponent (skillBase, obj);
		}

		private Component copyComponent(Component original, GameObject destination)
		{
			System.Type type = original.GetType();
			Component copy = destination.AddComponent(type);
			// Copied fields can be restricted with BindingFlags
			System.Reflection.FieldInfo[] fields = type.GetFields(); 
			foreach (System.Reflection.FieldInfo field in fields)
			{
				field.SetValue(copy, field.GetValue(original));
			}
			return copy;
		}
	}
}
