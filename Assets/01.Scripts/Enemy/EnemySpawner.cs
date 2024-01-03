using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    private float _spawnPeriod = 10f;
    private float _timer = 0f;
    private List<EnemyStatusSO> enemyStatusSOList;
    private List<GameObject> _enemyList;
    [SerializeField]
    private int _spawnCount = 10;

    [SerializeField]
    private List<Transform> _spawnPoints;

    private void Update()
    {
        if(_enemyList.Count <= 0)
        {
            _spawnCount++;
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
}
