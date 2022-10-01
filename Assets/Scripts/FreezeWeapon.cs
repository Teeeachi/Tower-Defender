
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeWeapon : MonoBehaviour
{
    public Transform target;
    private float distance;
    public string enemyTag;
    public float fr = 10;
    bool destroy = false;
    private bool towerDidHitEnemy;
    private float hitStrength;
    public float price;

    //Upgrades
    public int level;
    private float secondsForShooting;
    // Start is called before the first frame update
    void Start()
    {
        secondsForShooting = 2f;
        level = 1;
        price = 75f;
        towerDidHitEnemy = false;
        enemyTag = "Enemy";
        hitStrength = 15f;
        distance = 35f;
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
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= distance)
        {
            target = nearestEnemy.transform;
        }

        if (nearestEnemy != null && shortestDistance <= distance && towerDidHitEnemy == false)
        {
            target = nearestEnemy.transform;
            towerDidHitEnemy = true;
            FreezeEnemy(nearestEnemy);
            StartCoroutine(shootTheEnemy(target.gameObject));

        }
    }

    public void upgradeTower()
    {
        ++level;
        hitStrength += 5f;
        secondsForShooting -= 0.25f;
    }

    private IEnumerator shootTheEnemy(GameObject hitEnemy)
    {
        hitEnemy.GetComponent<Navmesh>().GotHit(hitStrength);
        yield return new WaitForSeconds(secondsForShooting);
        towerDidHitEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        if (Vector3.Distance(transform.position, target.position) <= distance)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * 100);
        }
    }
    void FreezeEnemy(GameObject enemy)
    {
        
        enemy.GetComponent<Navmesh>().FreezeMe();
    }

}

