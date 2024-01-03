using System.Collections.Generic;
using UnityEngine;

class Pool<T> where T : PoolableMono
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab; //�������� ����
    private Transform _parent;
    private PoolingType _poolingType;

    public Pool(T prefab, PoolingType poolingType, Transform parent, int count = 10)
    {
        _prefab = prefab;
        _parent = parent;
        _poolingType = poolingType;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.poolingType = _poolingType;
            obj.gameObject.name = _poolingType.ToString();
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T Pop()
    {
        T obj = null;
        if (_pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = _poolingType.ToString();
            obj.poolingType = _poolingType;
        }
        else
        {
            obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void Push(T obj)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}