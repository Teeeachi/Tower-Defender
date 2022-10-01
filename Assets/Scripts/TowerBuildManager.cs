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
    public CoinManager coins;
    public float price;
    public GameObject towerToBuild;
    public bool isNearATower;
    public bool hasTower;
    public Sprite updateImage;
    public Sprite buildImage;
    public string towerType;

    private GameObject ownTower;

    void Start()
    {
        ownTower = null;
        hasTower = false;
        if(towerType == "freeze")
        {
            price = towerToBuild.GetComponent<FreezeWeapon>().price;
        }
        else
        {
            price = towerToBuild.GetComponent<TowerWeapon>().price;
        }
        coins = GameObject.Find("CoinText").GetComponent<CoinManager>();
        playerTarget = GameObject.FindGameObjectWithTag("ThePlayer").transform;
        distance = 10f;
    }

    void Update()
    {
                if (!hasTower)
                {
                    buildButton.gameObject.transform.Find("BuildText").GetComponent<TextMeshProUGUI>().text = "Build for " + price;
                    buildButton.gameObject.transform.Find("Image").GetComponent<Image>().sprite = buildImage;
                    if (towerType == "freeze")
                    {
                        buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Freeze Tower";
                    }
                    else
                    {
                        buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Tower";
                    }
                }
                else
                {
                    buildButton.gameObject.transform.Find("BuildText").GetComponent<TextMeshProUGUI>().text = "Upgrade for " + price;
                    buildButton.gameObject.transform.Find("Image").GetComponent<Image>().sprite = updateImage;
                    if (towerType == "freeze")
                    {
                        buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Freeze Tower - Level " + ownTower.GetComponent<FreezeWeapon>().level;
                    }
                    else
                    {
                        buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Tower - Level " + ownTower.GetComponent<TowerWeapon>().level;
                    }
                }
    }

    public void onBuiltButtonClick()
    {
        if (!hasTower && coins.checkIfHasEnough(price))
        {
            hasTower = true;
            coins.removeAmount(price);
            ownTower = Instantiate(towerToBuild, transform.position + new Vector3(0, 10f, 0), Quaternion.identity);
            buildButton.gameObject.transform.Find("Image").GetComponent<Image>().sprite = updateImage;
            buildButton.gameObject.transform.Find("BuildText").GetComponent<TextMeshProUGUI>().text = "Upgrade for " + price;
            if (towerToBuild.GetComponent<TowerWeapon>() == null)
            {
                buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Freeze Tower - Level 1";
            }
            else
            {
                buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Tower - Level 1";
            }
        }
        else if(coins.checkIfHasEnough(price))
        {
            if (towerType == "freeze")
            {
                if(ownTower.GetComponent<FreezeWeapon>().level < 5)
                {
                    coins.removeAmount(price);
                    ownTower.GetComponent<FreezeWeapon>().upgradeTower();
                    buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Freeze Tower - Level " + ownTower.GetComponent<FreezeWeapon>().level;
                    Debug.Log("Upgraded!");
                }
                else
                {
                    Debug.Log("Max Level!");
                }
            }
            else
            {
                if (ownTower.GetComponent<TowerWeapon>().level < 5)
                {
                    coins.removeAmount(price);
                    ownTower.GetComponent<TowerWeapon>().upgradeTower();
                    buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Tower - Level " + ownTower.GetComponent<TowerWeapon>().level;
                    Debug.Log("Upgraded!");
                }
                else
                {
                    Debug.Log("Max Level!");
                }
            }
        }
    }
}
