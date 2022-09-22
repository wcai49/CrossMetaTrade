using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;

public class GameManagerSystem : MonoBehaviourPunCallbacks

{
    public GameObject malePrefab;
    public GameObject femalePrefab;
    public GameObject vcam_object;

    public GameObject phoneControlCanvas;
    public GameObject tradingCanvas;

    public GameObject backpackCanvas;
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
        var vcam = vcam_object.GetComponent<CinemachineFreeLook>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
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

    public void startSell()
    {
        player.GetComponent<PlayerControl>().StartSell(sellingBtn);
    }

    public void StartTrading()
    {
        // 1. Pop up trading UI
        // 2. Display NFT info
        // 3. Buy -> wallet spend amount
        tradingCanvas.SetActive(true);
        Cursor.visible = true;
    }

    public void StopTrading()
    {
        // Hide trading UI
        tradingCanvas.SetActive(false);
        Cursor.visible = backpackCanvas.activeInHierarchy;
    }

    public void placeOrder()
    {
        bool result = backpackCanvas.GetComponent<WalletDisplay>().wallet.spend("Bitcoin", 2);

        if (result)
        {
            tradingCanvas.GetComponent<TradingDisplay>().nft.AddOffers(PhotonNetwork.NickName);
            tradingCanvas.GetComponent<TradingDisplay>().updateOffer();
            backpackCanvas.GetComponent<WalletDisplay>().updateWallet();
        }

    }
}
