using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField]
    private Rigidbody2D _rigidbody2d;
    [SerializeField]
    private float _bulletSpeed = 5f;

    private void Start()
    {
        _rigidbody2d.velocity = transform.right * _bulletSpeed;
    }

    public override void Init()
    {
        
    }
}
