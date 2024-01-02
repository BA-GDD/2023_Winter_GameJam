using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected GunSO gunScriptableObject;
    protected float shootDelayTimer;
    protected float usableCapacity;

    protected virtual void Awake()
    {
        shootDelayTimer = gunScriptableObject.shootDelay;
        usableCapacity = gunScriptableObject.maximumCapacity;
    }

    protected virtual void Update()
    {
        shootDelayTimer -= Time.deltaTime;
    }

    public abstract void Shoot();

    public virtual void Reload()
    {
        usableCapacity += gunScriptableObject.fillCapacityPerSecond * Time.deltaTime;
        usableCapacity = Mathf.Clamp(usableCapacity, 0f, gunScriptableObject.maximumCapacity);
    }

    protected bool CanShoot()
    {
        return usableCapacity >= gunScriptableObject.useCapacityPerShoot && shootDelayTimer <= 0f;
    }
}
