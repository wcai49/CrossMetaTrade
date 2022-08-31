using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class GameManagerSystem : MonoBehaviourPunCallbacks

{
    public GameObject playerPrefab;
    public GameObject vcam_object;

    private void Start()
    {
        Cursor.visible = false;

        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(1f, 0.9f, 0f), Quaternion.identity);
        var vcam = vcam_object.GetComponent<CinemachineFreeLook>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
    }
}
