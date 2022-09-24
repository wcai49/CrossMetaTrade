using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new NFT", menuName = "nft")]
public class NFTs : ScriptableObject
{
    public Sprite NFT_Image;
    public string Details;
    public string nft_name;
    public string ownership;
    public double price;
    public Sprite Currency_icon;
    public Sprite graph;
    public string offers;


    public void AddOffers(string newOffer)
    {
        offers += " " + newOffer;
    }
}
