using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace BSS {
	[RequireComponent(typeof(PhotonView))]
	public class InRoomCustomChat : Photon.MonoBehaviour
	{
		public const int MESSAGE_MAX = 100;
        public Text PlayersText;
		public Text ChatText;
		public InputField inputField;
		public bool IsVisible = true;
		public List<string> messages = new List<string>();
		public string inputLine {
			get;set;
		}
		public static readonly string ChatRPC = "Chat";

        void Start() {
            updatePlayers();
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
					senderName = "Player " + mi.sender.ID;
				}
			}

			this.messages.Add(senderName +": " + newLine);
			updateChat ();
		}

		public void AddLine(string newLine)
		{
			this.messages.Add(newLine);
		}

        public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
            updatePlayers();
        }

		private void updateChat() {
			ChatText.text = "";
			int startIt = Mathf.Max (0, messages.Count - MESSAGE_MAX);
			for (int i = startIt; i < messages.Count; i++) {
				ChatText.text += messages [i] + "\n";
			}
		}
        private void updatePlayers() {
            if (PlayersText == null) {
                return;
            }
            var players = PhotonNetwork.playerList;
            foreach (var player in players) {
                PlayersText.text = "";
                PlayersText.text += "Player " + player.ID.ToString() + "\n";
            }
        }
	}
}