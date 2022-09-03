using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public float MaxHealth;
    public Slider HealthBarSlider;
    public float HealthDamageChangeSpeed;
    float currentHealth;
    bool didGetHit;
    float currentDealingDamage;
    public Image FillImage;
    // Start is called before the first frame update
    void Start()
    {
        currentDealingDamage = 0;
        didGetHit = false;
        HealthDamageChangeSpeed = 2f;
        MaxHealth = 100f;
        HealthBarSlider.maxValue = MaxHealth;
        HealthBarSlider.value = MaxHealth;
        currentHealth = HealthBarSlider.value;
    }

    public void gotHit(float damage)
    {
        currentDealingDamage += damage;
        currentHealth -= damage;
        didGetHit = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (didGetHit)
        {
            float DamagePerFrame = currentDealingDamage * Time.deltaTime * HealthDamageChangeSpeed;
            if(currentDealingDamage - DamagePerFrame > 0 && currentDealingDamage > 0.2f)
            {
                Color color = new Color((MaxHealth - HealthBarSlider.value)/100, (HealthBarSlider.value) / 100, 0);
                HealthBarSlider.value -= DamagePerFrame;
                HealthBarSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
                currentDealingDamage -= DamagePerFrame;
            }
            else
            {
                HealthBarSlider.value -= currentDealingDamage;
                currentDealingDamage = 0;
            }
            if(currentDealingDamage == 0)
            {
                didGetHit = false;
            }
        }
    }

}
