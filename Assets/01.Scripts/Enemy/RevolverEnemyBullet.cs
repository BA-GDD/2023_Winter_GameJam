using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverEnemyBullet : PoolableMono
{
    private Rigidbody2D rb;

    public override void Init()
    {
    }

    void Update()
    {
        rb.velocity = transform.right * 3000f;
    }
}
