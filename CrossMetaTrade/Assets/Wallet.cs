using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new wallet", menuName = "wallet")]
public class Wallet : ScriptableObject
{
    public string currency_bitcoin;
    public string currency_ethereum;

    public Sprite bitcoin_sprite;
    public Sprite ethereum_sprite;

    public double bitcoin_quantity;
    public double ethereum_quantity;

    public int NFT_num = 1;


    public bool spend(string currency, double amount)
    {
        bool result = false;
        if (currency == null || amount <= 0)
        {
            return result;
        }
        switch (currency)
        {
            case "Bitcoin":
                if (bitcoin_quantity >= amount)
                {
                    bitcoin_quantity -= amount;
                    result = true;
                }
                break;
            case "Ethereum":
                if (ethereum_quantity >= amount)
                {
                    ethereum_quantity -= amount;
                    result = true;
                }
                break;
            default:
                break;
        }
        return result;
    }

    public void gain(string currency, double amount)
    {
        if (currency == null || amount <= 0)
        {
            return;
        }
        switch (currency)
        {
            case "Bitcoin":
                bitcoin_quantity += amount;
                break;
            case "Ethereum":
                ethereum_quantity += amount;
                break;
            default:
                break;
        }
    }

    public void getNFT(int amount)
    {
        NFT_num += amount;
    }

    public void sellNFT(int amount)
    {
        NFT_num -= amount;
    }

}
