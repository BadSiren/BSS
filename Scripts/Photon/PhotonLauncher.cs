using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using BSS.UI;

namespace BSS {
	public class PhotonLauncher: Photon.PunBehaviour {
		public string version="1.0";
		public byte maxCount=2;
		public string sceneName="";

		public string waitText="다른 유저를 기다리는 중입니다.";


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


		public void RandomJoin()
		{
			PhotonNetwork.JoinRandomRoom ();
		}
		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
			PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxCount}, null);
			StartCoroutine (coRoomMaxCheck ());
			//Room Wait UI
			var enterBoard = (Board.boardList.Find (x => x is EnterSelect) as EnterSelect);
			enterBoard.Close ();
			(Board.boardList.Find (x => x is NoticeBoard) as NoticeBoard).Show (waitText,()=>{
				PhotonNetwork.LeaveRoom ();
				enterBoard.Show ();
			});
		}
			

		IEnumerator coRoomMaxCheck() {
			while (true) {
				yield return new WaitForSeconds (1f);
				if (PhotonNetwork.room==null) {
					break;
				}
				if (PhotonNetwork.room.PlayerCount==PhotonNetwork.room.MaxPlayers) {
					photonView.RPC ("recvLoadLevel", PhotonTargets.All);
					break;
				}
			}
		}

		[PunRPC]
		void recvLoadLevel(PhotonMessageInfo mi) {
			PhotonNetwork.LoadLevel (sceneName);
		}
	

	}
}
