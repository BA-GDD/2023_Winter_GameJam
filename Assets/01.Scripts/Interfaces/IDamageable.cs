using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    protected UnityEvent OnDieTrigger { get; }

    public void OnHit()
    {
        OnDieTrigger?.Invoke();
    }
}
