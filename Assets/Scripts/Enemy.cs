using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
   // NavMeshAgent agent;
    public Transform player;
    public double dist;
    public float dist1;
    public float Radius = 7;
    
    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = target.position;
        dist1 = Vector3.Distance(target.transform.position, transform.position);
        dist = Vector3.Distance(player.transform.position, transform.position);
        if ((dist > Radius)&&(dist1 > 2f))
        {
            transform.LookAt(target.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        else if ((dist < Radius) && (dist > 1f))
        {
            transform.LookAt(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if ((dist1 <= 2)&&(dist>Radius))
        {
            transform.LookAt(target.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
            gameObject.GetComponent<Animator>().SetTrigger("jab");
        }
        if (dist <= 2f)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }

    }
}
