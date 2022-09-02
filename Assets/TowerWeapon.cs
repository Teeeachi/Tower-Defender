
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    public Transform target;
    private float distance;
    public string enemyTag = "target";
    public float fr = 10;
    bool destroy = false;

    // Start is called before the first frame update
    void Start()
    {
        distance = 40f;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
        }
        if(nearestEnemy != null && shortestDistance <= distance)
        {
            target = nearestEnemy.transform;
        }

        if (Vector3.Distance(transform.position, target.position) <= distance)
        {


                Destroy(nearestEnemy,1f);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        if (Vector3.Distance(transform.position, target.position) <= distance)
        {
            Debug.Log("jbfhbdzxh");
            //transform.LookAt(target.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * 100);
            
        }


    }
    
}

