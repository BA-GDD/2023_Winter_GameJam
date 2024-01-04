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
    private List<Transform> _pointsAroundPlayer;

    [SerializeField]
    private List<Transform> _pointsMap;

    [SerializeField]
    private WarningMark _warningMark;

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
        Vector2 spawnPos = Vector2.zero;

        for (int i = 0; i < _pointsAroundPlayer.Count; ++i)
        {
            int randNum = Random.Range(0, _pointsAroundPlayer.Count);
            spawnPos = _pointsAroundPlayer[randNum].position;
            spawnPos.x = Mathf.Clamp(spawnPos.x, -33, 35);
            spawnPos.y = Mathf.Clamp(spawnPos.y, -40, 41);
            if (MapManager.Instance.CheckWater(spawnPos, out Vector3Int pos))
            {
                print("여긴 물이야");
                spawnPos = Vector2.zero;
                continue;
            }
            else
            {
                print($"물이 아니어서 여기다가 소환할게 : {_pointsAroundPlayer[randNum].name}");
                break;
            }
        }

        Transform trm = _pointsMap[Random.Range(0, _pointsMap.Count)];
        if (spawnPos == Vector2.zero)
        {
            spawnPos = trm.position;
        }

        _warningMark.gameObject.SetActive(true);
        _warningMark.SetUp(spawnPos);

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
}
