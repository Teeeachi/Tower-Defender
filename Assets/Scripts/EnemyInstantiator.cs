using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        StartCoroutine("spawningEnemies");
    }

    private IEnumerator spawningEnemies()
    {
        while (true)
        {
            Instantiate(enemy, new Vector3(80, 0, 80), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
