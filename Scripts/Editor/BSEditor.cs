using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BSS {
	public class BSEditor : EditorWindow
	{
		int mainToolbarIndex = 0;
		string[] mainToolbars = {"Unit", "LobbyItem", "toolbar3"};

		[MenuItem("Window/BSEditor")]
		public static void ShowWindow() {
			EditorWindow.GetWindow<BSEditor>();
		}
		void OnGUI () {
			GUILayout.BeginArea(new Rect(0, 0, 400, 400));
			GUILayout.BeginHorizontal();
			mainToolbarIndex=GUILayout.Toolbar (mainToolbarIndex, mainToolbars);
			GUILayout.EndHorizontal();
			switch (mainToolbarIndex) {
			case 0:
				
				break;
			case 1:
				GUILayout.Button ("Create");
				break;
			}


			GUILayout.EndArea();
		}
	}
}

