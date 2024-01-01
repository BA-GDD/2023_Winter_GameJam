using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField]
    private GunSO gunScriptableObject;

    public abstract void Reload();

    public abstract void Shoot();
}
