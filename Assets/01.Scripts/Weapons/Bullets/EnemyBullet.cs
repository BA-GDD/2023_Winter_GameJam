using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.OnHitHandle();
            PoolManager.Instance.Push(this);
        }
    }
}
