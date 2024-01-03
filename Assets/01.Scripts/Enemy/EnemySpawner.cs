using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [SerializeField]
    private List<EnemyStatusSO> enemyStatusSOList;
    private int _enemyCount;
    [SerializeField]
    private int _spawnCount = 10;

    [SerializeField]
    private List<Transform> _spawnPoints;

    private void Start()
    {
        for(int i = 0; i < _spawnCount; ++i)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        if(_enemyCount <= 0)
        {
            for(int i = 0; i < _spawnCount; ++i)
            {
                SpawnEnemy();
            }
        }

        //_timer += Time.deltaTime;
        //if(_timer >= _spawnPeriod)
        //{
        //    float percent = Random.value * 100f;
        //    for(int i = 0; i < enemyStatusSOList.Count; ++i)
        //    {
        //        if(percent < enemyStatusSOList[i].spawnPercent)
        //        {
        //            PoolManager.Instance.Pop(enemyStatusSOList[i].type);
        //        }
        //        else
        //        {
        //            percent -= enemyStatusSOList[i].spawnPercent;
        //        }
        //    }
        //}
    }

    public void SpawnEnemy()
    {
        float percent = Random.value * 100f;
        for (int i = 0; i < enemyStatusSOList.Count; ++i)
        {
            if (percent < enemyStatusSOList[i].spawnPercent)
            {
                int rand = Random.Range(0, 7);
                PoolableMono enemy = PoolManager.Instance.Pop(enemyStatusSOList[i].type);
                enemy.transform.position = _spawnPoints[i].position;
            }
            else
            {
                percent -= enemyStatusSOList[i].spawnPercent;
            }
        }
    }

    public void DeSpawnEnemy(PoolableMono item)
    {
        _enemyCount--;
        PoolManager.Instance.Push(item);
    }
}
