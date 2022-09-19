using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class GameManagerSystem : MonoBehaviourPunCallbacks

{
    public GameObject malePrefab;
    public GameObject femalePrefab;
    public GameObject vcam_object;

    private void Start()
    {
        Cursor.visible = false;
        GameObject player;
        if(PhotonNetwork.LocalPlayer.CustomProperties["Gender"] == "male")
        {
          player = PhotonNetwork.Instantiate(malePrefab.name, new Vector3(1f, 0.9f, 0f), Quaternion.identity);
        }
        else
        {
          player = PhotonNetwork.Instantiate(femalePrefab.name, new Vector3(1f, 0.9f, 0f), Quaternion.identity);
        }
        var vcam = vcam_object.GetComponent<CinemachineFreeLook>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
    }
}
