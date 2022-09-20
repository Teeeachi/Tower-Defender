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

    private GameObject ownTower;

    void Start()
    {
        ownTower = null;
        hasTower = false;
        if(towerToBuild.GetComponent<TowerWeapon>() == null)
        {
            price = towerToBuild.GetComponent<FreezeWeapon>().price;
        }
        else
        {
            price = towerToBuild.GetComponent<TowerWeapon>().price;
        }
        
        buildButton = GameObject.FindGameObjectWithTag("BuildButton").GetComponent<Button>();
        coins = GameObject.Find("CoinText").GetComponent<CoinManager>();
        playerTarget = GameObject.FindGameObjectWithTag("ThePlayer").transform;
        isNearATower = false;
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
                if (!hasTower)
                {
                    buildButton.gameObject.transform.Find("BuildText").GetComponent<TextMeshProUGUI>().text = "Build for " + price;
                    buildButton.gameObject.transform.Find("Image").GetComponent<Image>().sprite = buildImage;
                    if (towerToBuild.GetComponent<TowerWeapon>() == null)
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
                    if (towerToBuild.GetComponent<TowerWeapon>() == null)
                    {
                        buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Freeze Tower - Level " + ownTower.GetComponent<FreezeWeapon>().level;
                    }
                    else
                    {
                        buildButton.gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Tower - Level " + ownTower.GetComponent<TowerWeapon>().level;
                    }
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
            if (towerToBuild.GetComponent<TowerWeapon>() == null)
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
