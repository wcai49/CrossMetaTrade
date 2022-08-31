using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class loadingManager : MonoBehaviourPunCallbacks
{
    public Slider slider;
    public int processSpeed;
    private int currentProcess;
    private int maxProcess = 100;
    private float timer = 0f;
    private bool isProcessDone = false;
    public void setProcess(int process)
    {
        slider.value = process;
    }

    private void Start()
    {
        currentProcess = 0;
        setProcess(currentProcess);
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (currentProcess < maxProcess)
        {
            if (timer >= 1f)
            {
                currentProcess += processSpeed;
                setProcess(currentProcess);
                timer = 0f;
            }
        }
        else
        {
            isProcessDone = true;
        }


    }

    public override void OnConnectedToMaster()
    {
        if (!isProcessDone)
        {
            setProcess(100);
        }
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
