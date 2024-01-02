using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    public UnityEvent onDieTrigger;

    public virtual void OnHit()
    {
        onDieTrigger.Invoke();
    }
}
