using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 point1;
    public Vector3 point2;
    public Vector3 point3;
    private int stageNum;
    private int waveCount;
    private float spawnSpeed;
    public GameMaster gameMaster;
    private bool didntWin;

    void Start()
    {
        didntWin = true;
        waveCount = 0;
        stageNum = 0;
        point1 = new Vector3(77, 0, 50);
        point2 = new Vector3(46, 0, 77.5f);
        point3 = new Vector3(63, 0, 116);
        spawnSpeed = 8f;
        StartCoroutine("spawningEnemies");
    }
    void Update()
    {
        if ((stageNum == 4) && (GameObject.FindGameObjectsWithTag("Enemy").Length==0) && didntWin)
        {
            didntWin = false;
            gameMaster.youWon();
        }
    }
    private IEnumerator spawningEnemies()
    {
        for (int i = 0; i < 34; i++)
        {
            if (stageNum == 0)
            {
                yield return new WaitForSeconds(1f);
                stageNum++;
            }
            else if (stageNum == 1)
            {
                Instantiate(enemy, point1, Quaternion.identity);
                yield return new WaitForSeconds(spawnSpeed);
                if (spawnSpeed >= 2.8f)
                {
                    spawnSpeed -= 0.2f;
                }
                waveCount++;
            }
            else if (stageNum == 2)
            {
                Instantiate(enemy, point1, Quaternion.identity);
                Instantiate(enemy, point2, Quaternion.identity);
                yield return new WaitForSeconds(spawnSpeed);
                if (spawnSpeed >= 2.8f)
                {
                    spawnSpeed -= 0.2f;
                }
                waveCount++;
            }
            else if (stageNum == 3)
            {
                Instantiate(enemy, point1, Quaternion.identity);
                Instantiate(enemy, point2, Quaternion.identity);
                Instantiate(enemy, point3, Quaternion.identity);
                yield return new WaitForSeconds(spawnSpeed);
                if (spawnSpeed >= 2.8f)
                {
                    spawnSpeed -= 0.2f;
                }
                waveCount++;
            }
            if (waveCount == 11)
            {
                waveCount = 0;
                if(stageNum <= 3)
                {
                    stageNum++;
                }
            }
        }
    }
}
