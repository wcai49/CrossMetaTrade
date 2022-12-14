using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    public Wallet wallet;

    public GameObject NFTs;
    public Image bitcoin_image;
    public Image ethereum_image;

    public TextMeshProUGUI bit_amount;
    public TextMeshProUGUI ether_amount;

    public TextMeshProUGUI NFT_Num;

    // Start is called before the first frame update
    void Start()
    {
        bitcoin_image.sprite = wallet.bitcoin_sprite;
        ethereum_image.sprite = wallet.ethereum_sprite;

        bit_amount.text = wallet.bitcoin_quantity.ToString();
        ether_amount.text = wallet.ethereum_quantity.ToString();
        NFT_Num.text = wallet.NFT_num.ToString();
    }

    public void updateWallet()
    {
        bit_amount.text = wallet.bitcoin_quantity.ToString();
        ether_amount.text = wallet.ethereum_quantity.ToString();
        NFT_Num.text = wallet.NFT_num.ToString();
    }

}
