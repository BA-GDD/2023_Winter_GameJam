using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolableMono
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private Rigidbody2D _rigid2d;

    public override void Init()
    {
        
    }

    private void Start()
    {
        _rigid2d.velocity = transform.right * _speed;
    }
}
