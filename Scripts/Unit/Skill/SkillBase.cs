using UnityEngine;
using System.Collections;
using BSS.Unit;

namespace BSS.Skill {
	public class SkillBase : MonoBehaviour
	{
		public string skillIndex;
		public Sprite skillImage;
		public string skillName;
		[TextArea()]
		public string _skillText;
		protected string replaceText;
		public string skillText {
			get {
				replaceTexting ();
				return replaceText;
			}
		}
		[HideInInspector]
		public int level {
			get {
				if (skillable == null) {
					return 0;
				}
				return skillable.level;
			}
		}
		public int maxLevel;

		protected BaseUnit owner;
		[HideInInspector]
		public Skillable skillable;

		protected virtual void onInitialize() {
			owner = GetComponent<BaseUnit> ();
		}

		protected virtual bool validate() {
			if (level == 0) {
				return false;
			}
			return true;
		}
		protected virtual void replaceTexting() {
			if (level == 0) {
				replaceText = "[배우지않음]";
				return;
			}
			replaceText= "레벨: "+level.ToString()+" / "+maxLevel.ToString()+"\n"+_skillText;
		}
	}
}

