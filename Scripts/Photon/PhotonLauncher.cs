using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BSS.UI;
using UnityEngine.UI;

namespace BSS {
	public class PhotonLauncher: Photon.PunBehaviour {
		public string version="1.0";
		
		public string sceneName="";

        public string disconnectText = "인터넷 연결이 불안정합니다. 잠시 후 다시 시도해주세요.";
		public string waitText="다른 유저를 기다리는 중입니다.";

        public Text connectCountText;
        public Text awaiterCountText;

        public bool isJoined;

        private byte currentCount;

		void Awake() {
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.ConnectUsingSettings (version);
			//StartCoroutine (coConnectTry());
		}
		IEnumerator coConnectTry() {
			while (true) {
				if (!PhotonNetwork.connected) {
					PhotonNetwork.ConnectUsingSettings (version);
				}
				yield return new WaitForSeconds (2f);
			}
		}

        public void SingleJoin() {
            if (!isJoined) {
                NoticeBoard.Notice(disconnectText);
                return;
            }
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 1 }, null);
        }
        public void DoubleJoin() {
            RandomJoin(2);
        }
        public void SixJoin() {
            RandomJoin(6);
        }


		public void RandomJoin(byte num)
		{
            if (!isJoined) {
                NoticeBoard.Notice(disconnectText);
                return;
            }

            currentCount = num;
            PhotonNetwork.JoinRandomRoom(null, currentCount);
            var enterBoard = (Board.boardList.Find(x => x is EnterSelect) as EnterSelect);
            enterBoard.Close();
            NoticeBoard.Notice(waitText, () => {
                PhotonNetwork.LeaveRoom();
                isJoined = false;
                enterBoard.Show();
            });
		}

        public override void OnConnectedToMaster() {
            isJoined = true;
        }


		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
            var roomOption = new RoomOptions {
                MaxPlayers = currentCount
            };
            PhotonNetwork.CreateRoom(null, roomOption, null);
			//Room Wait UI
			var enterBoard = (Board.boardList.Find (x => x is EnterSelect) as EnterSelect);
			enterBoard.Close ();
            NoticeBoard.Notice(waitText, () => {
                PhotonNetwork.LeaveRoom();
                isJoined = false;
                enterBoard.Show();
            });
		}
        public override void OnLobbyStatisticsUpdate() {
            if (connectCountText != null) {
                connectCountText.text = PhotonNetwork.countOfPlayers.ToString();
            }
            if (awaiterCountText != null) {
                awaiterCountText.text = PhotonNetwork.countOfPlayersOnMaster.ToString();
            }
        }

        public override void OnJoinedRoom() {
            if (PhotonNetwork.isMasterClient && PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers) {
                PhotonNetwork.room.IsOpen = false;
                photonView.RPC("recvLoadLevel", PhotonTargets.All);
            }
        }


		[PunRPC]
		void recvLoadLevel(PhotonMessageInfo mi) {
			PhotonNetwork.LoadLevel (sceneName);
		}
	

	}
}
