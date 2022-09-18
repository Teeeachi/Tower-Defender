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

    void Start()
    {
        coinCount = 100f;
        coinText.text = "Coins: " + coinCount;
    }

    void Update()
    {

    }
}
