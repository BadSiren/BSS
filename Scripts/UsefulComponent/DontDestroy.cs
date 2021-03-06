﻿using UnityEngine;
using System.Collections;

namespace BSS {
	public class DontDestroy : MonoBehaviour
	{
		private static GameObject instance;
		// Use this for initialization
		void Awake ()
		{
			if (instance == null) {
				instance = gameObject;
                DontDestroyOnLoad(gameObject);
			} else {
				Destroy (gameObject);
				return;
			}
		}

	}
}

