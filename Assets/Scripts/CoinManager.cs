using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private float coinCount;

    public bool checkIfHasEnough(float price)
    {
        if (coinCount >= price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void removeAmount(float price)
    {
        coinCount -= price;
        coinText.text = "Coins: " + coinCount;
    }

    public void addAmount(float amount)
    {
        coinCount += amount;
        coinText.text = "Coins: " + coinCount;
    }

    void Start()
    {
        coinCount = 250f;
        coinText.text = "Coins: " + coinCount;
    }

    void Update()
    {

    }
}
