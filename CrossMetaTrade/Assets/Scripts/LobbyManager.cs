using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField lobbyInput;
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(lobbyInput.text);
    }

    public void JoinRoom()
    {
        Debug.Log("Joining");
        PhotonNetwork.JoinRoom(lobbyInput.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");
        PhotonNetwork.LoadLevel("MetaWorld");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode);
        Debug.Log(message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList);
    }
}
