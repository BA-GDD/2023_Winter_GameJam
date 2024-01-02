using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField]
    private Rigidbody2D _rigidody2d;
    [SerializeField]
    private float _bulletSpeed = 5f;

    private void Start()
    {
        _rigidody2d.velocity = transform.right * _bulletSpeed;
    }

    public override void Init()
    {
        
    }
}
