using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerSystem : MonoBehaviourPunCallbacks

{
    public GameObject malePrefab;
    public GameObject femalePrefab;
    public GameObject vcam_object;

    public GameObject phoneControlCanvas;
    public GameObject tradingCanvas;

    GameObject backpackCanvas;
    public Button sellingBtn;

    GameObject player;
    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        phoneControlCanvas.SetActive(true);
#else
        phoneControlCanvas.SetActive(false);
#endif
    }

    private void Start()
    {

        if (PhotonNetwork.LocalPlayer.CustomProperties["Gender"] == "male")
        {
            player = PhotonNetwork.Instantiate(malePrefab.name, new Vector3(1f, 0.9f, 0f), Quaternion.identity);
        }
        else
        {
            player = PhotonNetwork.Instantiate(femalePrefab.name, new Vector3(1f, 0.9f, 0f), Quaternion.identity);
        }
        player.GetComponent<PlayerControl>().toggleBackpack();
        backpackCanvas = player.transform.Find("BackpackCanvas").gameObject;
        var vcam = vcam_object.GetComponent<CinemachineFreeLook>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
    }

    private void Update()
    {
        if (player == null)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Loading");
        }
    }
    public void toggleBackpack()
    {
        player.GetComponent<PlayerControl>().toggleBackpack();
    }
    public void showCursor()
    {
        Cursor.visible = true;
    }

    public void hideCursor()
    {
        Cursor.visible = false;
    }

    public void ShowTradingCanvas()
    {
        // 1. Pop up trading UI
        // 2. Display NFT info
        // 3. Buy -> wallet spend amount
        tradingCanvas.SetActive(true);
        showCursor();
    }

    public void HideTradingCanvas()
    {
        // Hide trading UI
        tradingCanvas.SetActive(false);
        Cursor.visible = backpackCanvas.activeInHierarchy;
    }

    public void placeOrder()
    {
        bool result = backpackCanvas.GetComponent<WalletDisplay>().wallet.spend("Bitcoin", 0.18);


        if (result)
        {
            backpackCanvas.GetComponent<WalletDisplay>().wallet.getNFT(1);
            tradingCanvas.GetComponent<TradingDisplay>().nft.AddOffers(PhotonNetwork.NickName);
            tradingCanvas.GetComponent<TradingDisplay>().updateOffer();
            backpackCanvas.GetComponent<WalletDisplay>().updateWallet();
        }

    }
}
