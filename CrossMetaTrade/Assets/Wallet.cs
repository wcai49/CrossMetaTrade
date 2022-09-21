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

    public int bitcoin_quantity;
    public int ethereum_quantity;


    public void spend(string currency, int amount)
    {
        if (currency == null || amount <= 0)
        {
            return;
        }
        switch (currency)
        {
            case "Bitcoin":
                bitcoin_quantity -= amount;
                break;
            case "Ethereum":
                ethereum_quantity -= amount;
                break;
            default:
                break;     
        }
    }

    public void gain(string currency, int amount)
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

}
