using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    public bool IsDead { get; set; }
    protected UnityEvent OnDieTrigger { get; }

    public void OnHit()
    {
        OnDieTrigger?.Invoke();
    }
}
