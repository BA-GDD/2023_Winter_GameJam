using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum PoolingType
{
    None,
    StatusInfoPanel,
    PlayerBullet,
    EnemyBullet,
    PlayerRazer,
    FallingMilk,
    RevolverEnemy,
    MeleeEnemy,
    SniperEnemy,
    ShieldEnemy,
    SpaSmoke,
    EnemyExplosion,
}

public class PoolManager
{
    public static PoolManager Instance;

    private Dictionary<PoolingType, Pool<PoolableMono>> _poolDic = new Dictionary<PoolingType, Pool<PoolableMono>>();

    private Transform _parentTrm;
    public PoolManager(Transform parentTrm)
    {
        _parentTrm = parentTrm;
        Instance = this;
    }

    public void CreatePool(PoolableMono prefab, PoolingType poolingType, int count = 10)
    {
        _poolDic.Add(poolingType, new Pool<PoolableMono>(prefab, poolingType, _parentTrm, count));
    }
    public void Push(PoolableMono obj)
    {
        if (_poolDic.ContainsKey(obj.poolingType))
            _poolDic[obj.poolingType].Push(obj);
        else
            Debug.LogError($"not have ${obj.name} pool");
    }
    public async void Push(PoolableMono obj, float second)
    {
        int delay = (int)(second * 1000);
        await Task.Delay(delay);
        _poolDic[obj.poolingType].Push(obj);
    }
    public PoolableMono Pop(PoolingType type)
    {
        PoolableMono obj = null;
        if (!_poolDic.ContainsKey(type))
        {
            Debug.LogError($"not have [${type.ToString()}] pool");
        }
        obj = _poolDic[type].Pop();
        return obj;
    }
}
