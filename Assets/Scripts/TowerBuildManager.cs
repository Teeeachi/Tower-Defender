using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerBuildManager : MonoBehaviour
{
    public Transform playerTarget;
    public float distance;
    public Button buildButton;
    public GameObject towersOwnWeapon;
    public CoinManager coins;
    public float price;
    public GameObject towerToBuild;

    public bool isNearATower;

    public Sprite updateImage;
    public Sprite buildImage;

    void Start()
    {
        price = 50f;
        buildButton = GameObject.FindGameObjectWithTag("BuildButton").GetComponent<Button>();
        coins = GameObject.Find("CoinText").GetComponent<CoinManager>();
        playerTarget = GameObject.FindGameObjectWithTag("ThePlayer").transform;
        isNearATower = false;
        towersOwnWeapon = null;
        distance = 10f;
        buildButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Vector3.Distance(playerTarget.transform.position, transform.position) <= distance)
        {
            buildButton.gameObject.SetActive(true);
            if (!isNearATower)
            {
                buildButton.onClick.AddListener(onBuiltButtonClick);
                if (!towersOwnWeapon)
                {
                    buildButton.gameObject.transform.Find("Image").GetComponent<Image>().sprite = buildImage;
                }
                else
                {
                    buildButton.gameObject.transform.Find("Image").GetComponent<Image>().sprite = updateImage;
                }
            }
            isNearATower = true;
        }
        else
        {
            isNearATower = false;
            buildButton.gameObject.SetActive(false);
        }
    }

    public void onBuiltButtonClick()
    {
        if (!towersOwnWeapon && coins.checkIfHasEnough(price))
        {
            coins.removeAmount(price);
            Debug.Log("Built!");
            towersOwnWeapon = Instantiate(towerToBuild, transform.position + new Vector3(0, 10f, 0), Quaternion.identity);
            buildButton.gameObject.transform.Find("Image").GetComponent<Image>().sprite = updateImage;
        }
        else
        {
            Debug.Log("Already has a Tower!");
        }
    }
}
