using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{


    public void SpawnEnemy(PoolingType enemyType)
    {
        PoolManager.Instance.Pop(enemyType);

        
    }
}
