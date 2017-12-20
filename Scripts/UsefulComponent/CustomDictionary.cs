using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS{
	public class CustomDictionary: SerializedMonoBehaviour
	{
		public Dictionary<string,Sprite> spriteDics=new Dictionary<string,Sprite>();
		public Dictionary<string,TextAreaString> textDics=new Dictionary<string,TextAreaString>();

		public Sprite getSprite(string key) {
			return spriteDics [key];
		}
		public string getText(string key) {
			return textDics [key];
		}

		[InlineProperty()]
		public struct TextAreaString
		{
			[TextArea()]
			public string Value;

			public static implicit operator TextAreaString(string obj)
			{
				return new TextAreaString() { Value = obj };
			}

			public static implicit operator string(TextAreaString obj)
			{
				return obj.Value;
			}
		}
	}
}

