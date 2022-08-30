using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class GameManagerSystem : MonoBehaviour

{
    public GameObject playerPrefab;
    private void Start()
    {
        Cursor.visible = false;

        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(1f, 0.9f, 0f), Quaternion.identity);

        if (PhotonView.Get(playerPrefab).IsMine)
        {
            var vcam = GetComponent<CinemachineVirtualCamera>();
            vcam.LookAt = this.playerPrefab.transform;
            vcam.Follow = this.playerPrefab.transform;
        }
    }
}
