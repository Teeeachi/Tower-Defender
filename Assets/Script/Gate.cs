using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public double dist;
    public GameObject newTower;
    public GameObject oldTower;
    public float HP = 5;
    public float StartTime;
    public float EndTime;
    void Start()
    {
        newTower.SetActive(false);
        oldTower.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist < 2)
        {
            if (StartTime <= 5)
            {
                StartTime += 1f * Time.deltaTime;
            }
            else if (HP > 0)
            {
                HP -= 1f;
                StartTime = 0;
            }
            else
            {
                newTower.SetActive(true);
                oldTower.SetActive(false);
            }
        }
         
    }
}
