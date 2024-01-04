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

    public bool isIndicateMark;
    [SerializeField]
    private Indicator indicator;

    private void Start()
    {

        SpawnEnemy();
    }

    private void Update()
    {
        if (_enemyCount <= 0)
        {
            SpawnEnemy();
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
        SetIndicateMark(true);
        Vector2 spawnPos = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
        for (int i = 0; i < _spawnCount; ++i)
        {
            RandomEnemy(spawnPos);
        }
    }
    private void RandomEnemy(Vector2 spawnPos)
    {
        float percent = Random.value * 100f;
        for (int i = 0; i < enemyStatusSOList.Count; ++i)
        {
            if (percent < enemyStatusSOList[i].spawnPercent)
            {
                PoolableMono enemy = PoolManager.Instance.Pop(enemyStatusSOList[i].type);
                enemy.transform.position = spawnPos + Random.insideUnitCircle * 3f;
                ++_enemyCount;
                break;
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

    public void SetIndicateMark(bool value)
    {
        Debug.Log(value);
        isIndicateMark = value;
    }
}
