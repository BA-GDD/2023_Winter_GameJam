using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour
{
    public PoolingType poolingType;
    public abstract void Init();
    public virtual void GotoPool()
    {
        PoolManager.Instance.Push(this);
    }
}
