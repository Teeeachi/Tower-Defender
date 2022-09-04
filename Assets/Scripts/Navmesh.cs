using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Navmesh : MonoBehaviour
{
    public double dist;
    public double dist1;
    private Transform player;
    public double Rad=6f;
    private Transform target;
    NavMeshAgent agent;

    public Slider sliderHP;
    private float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
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
        if ((dist > Rad)&&(dist1>1.5f))
        {
            agent.destination = target.position;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if ((dist < Rad)&&(dist>1.5f))
        {
            agent.destination = player.position;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if ((dist > Rad) && (dist1 < 1.5f))
        {
            transform.LookAt(target.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
            gameObject.GetComponent<Animator>().SetTrigger("jab");
        }
        if (dist <= 3f)
        {
            transform.LookAt(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
            gameObject.GetComponent<Animator>().SetTrigger("jab");          
        }
    }
    public void GotHit(float damage)
    {
        currentHealth -= damage;
        sliderHP.value = currentHealth / maxHealth;
        //Debug.Log(damage);
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
