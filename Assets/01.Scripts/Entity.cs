using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : PoolableMono
{
    public UnityEvent onDieTrigger;

    public virtual void OnHit()
    {
        onDieTrigger.Invoke();
    }
}
