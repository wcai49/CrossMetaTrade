using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField lobbyInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(lobbyInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(lobbyInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SpaceShip");
    }
}
