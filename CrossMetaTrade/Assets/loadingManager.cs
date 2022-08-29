using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class loadingManager : MonoBehaviour
{
    public Slider slider;
    public int processSpeed = 1;
    private int currentProcess;
    private int maxProcess = 100;
    private float timer = 0f;
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

        if (currentProcess >= maxProcess && PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("SpaceShip");
        }
        else
        {
            if (PhotonNetwork.IsConnected)
            {
                processSpeed = 20;
            }
            else
            {
                processSpeed = 0;
            }
        }

        if (timer >= 1f)
        {
            currentProcess += processSpeed;
            setProcess(currentProcess);
            timer = 0f;
        }
    }
}
