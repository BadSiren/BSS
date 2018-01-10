using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace BSS {
	[RequireComponent(typeof(PhotonView))]
	public class InRoomCustomChat : Photon.MonoBehaviour
	{
		public const int MESSAGE_MAX = 5;
		public Text ChatText;
		public InputField inputField;
		public bool IsVisible = true;
		public List<string> messages = new List<string>();
		public string inputLine {
			get;set;
		}
		private Vector2 scrollPos = Vector2.zero;
		private Rect GuiRect;

		public static readonly string ChatRPC = "Chat";


		public void OnGUI()
		{
			if (!this.IsVisible || !PhotonNetwork.inRoom)
			{
				return;
			}
				
			GUI.SetNextControlName("");
			GUILayout.BeginArea(this.GuiRect);

			scrollPos = GUILayout.BeginScrollView(scrollPos);
			GUILayout.FlexibleSpace();
			for (int i = messages.Count - 1; i >= 0; i--)
			{
				GUILayout.Label(messages[i]);
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
		public void SendChat() {
			if (string.IsNullOrEmpty (inputLine)) {
				return;
			}
			photonView.RPC("Chat", PhotonTargets.All, inputLine);
			inputLine = "";
			inputField.text = "";
		}

		[PunRPC]
		public void Chat(string newLine, PhotonMessageInfo mi)
		{
			string senderName = "anonymous";

			if (mi.sender != null)
			{
				if (!string.IsNullOrEmpty(mi.sender.NickName))
				{
					senderName = mi.sender.NickName;
				}
				else
				{
					senderName = "player " + mi.sender.ID;
				}
			}

			this.messages.Add(senderName +": " + newLine);
			updateChat ();
		}

		public void AddLine(string newLine)
		{
			this.messages.Add(newLine);
		}

		private void updateChat() {
			ChatText.text = "";
			int startIt = Mathf.Max (0, messages.Count - MESSAGE_MAX);
			for (int i = startIt; i < messages.Count; i++) {
				ChatText.text += messages [i] + "\n";
			}
		}
	}
}