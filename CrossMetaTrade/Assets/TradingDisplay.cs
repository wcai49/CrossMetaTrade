using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradingDisplay : MonoBehaviour
{
    public NFTs nft;
    public Image nft_image;
    public Image currency_image;
    public Image linechart;

    public TextMeshProUGUI details;
    public TextMeshProUGUI nft_name;

    public TextMeshProUGUI nft_price;

    public TextMeshProUGUI nft_ownership;

    public TextMeshProUGUI nft_offers;
    // Start is called before the first frame update
    void Start()
    {
        nft_image.sprite = nft.NFT_Image;
        currency_image.sprite = nft.Currency_icon;
        linechart.sprite = nft.graph;
        details.text = nft.Details;
        nft_name.text = nft.nft_name;
        nft_price.text = nft.price.ToString();
        nft_ownership.text = nft.ownership;
        nft_offers.text = nft.offers;
    }

    public void updateOffer()
    {
        nft_offers.text = nft.offers;
    }
}
