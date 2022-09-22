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


    public bool spend(string currency, int amount)
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
