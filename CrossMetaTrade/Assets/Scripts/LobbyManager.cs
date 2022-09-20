using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField lobbyInput;
    public InputField userNameInput;
    public bool isMale;

    private ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();

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
        _playerProperties["Gender"] = isMale ? "male" : "female";
        PhotonNetwork.LocalPlayer.CustomProperties = _playerProperties;
        PhotonNetwork.NickName = userNameInput.text + " " + Random.Range(0, 1000).ToString("0000");
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

    public void setCurrentCharacter(bool newGender)
    {
      Debug.Log("Current:" + newGender);
      isMale = newGender;
    }
}
