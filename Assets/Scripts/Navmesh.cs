using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Navmesh : MonoBehaviour
{
    public HealthBarController TowerHealthBarSlider;
    public HealthBarController PlayerHealthBarSlider;
    public PlayerController thePlayer;
    public double dist;
    public double dist1;
    private Transform player;
    public double Rad;
    private Transform target;
    NavMeshAgent agent;

    public Slider sliderHP;
    private float maxHealth;
    private float currentHealth;

    private bool didHit;

    bool frozen = false;

    public CoinManager coins;
    public float coinAmount;
    // Start is called before the first frame update
    void Start()
    {
        coinAmount = 25f;
        coins = GameObject.Find("CoinText").GetComponent<CoinManager>();
        frozen = false;
        didHit = false;
        thePlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        TowerHealthBarSlider = GameObject.Find("TowerSlider").GetComponent<HealthBarController>();
        PlayerHealthBarSlider = GameObject.Find("PlayerSlider").GetComponent<HealthBarController>();
        Rad = 12f;
        maxHealth = 100f;
        currentHealth = 100f;
        player = GameObject.FindWithTag("ThePlayer").transform;
        target = GameObject.FindWithTag("Tower").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        dist1 = Vector3.Distance(target.transform.position, transform.position);
        dist = Vector3.Distance(player.transform.position, transform.position);
        if ((dist > Rad)&&(dist1>3f))
        {
            agent.destination = target.position;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if ((dist < Rad)&&(dist>3f))
        {
            agent.destination = player.position;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if ((dist > Rad) && (dist1 <= 3f) && didHit == false)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
            if (didHit == false)
            {
                gameObject.GetComponent<Animator>().SetTrigger("jab");
                StartCoroutine(AttackTheTower(5f));
                didHit = true;
            }
            //transform.LookAt(target.transform.position);
        }
        if ((dist <= 6f) && didHit == false)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
            if(didHit == false)
            {
                gameObject.GetComponent<Animator>().SetTrigger("jab");
                StartCoroutine(AttackThePlayer(5f));
                didHit = true;
            }
            //transform.LookAt(player.transform.position);         
        }
    }

    private IEnumerator AttackTheTower(float damage)
    {
        TowerHealthBarSlider.gotHit(damage);
        yield return new WaitForSeconds(3f);
        didHit = false;
    }

    private IEnumerator AttackThePlayer(float damage)
    {
        PlayerHealthBarSlider.gotHit(damage);
        thePlayer.GotHitByAnEnemy();
        yield return new WaitForSeconds(3f);
        didHit = false;
    }

    public void GotHit(float damage)
    {
        currentHealth -= damage;
        sliderHP.value = currentHealth / maxHealth;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            coins.addAmount(coinAmount);
        }
    }
    public void FreezeMe()
    {
        if (!frozen)
        {
            frozen = true;
            GetComponent<NavMeshAgent>().speed /= 2;
        }

    }
}
