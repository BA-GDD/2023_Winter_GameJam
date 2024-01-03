using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MobBrain brain))
        {
            brain.OnHitHandle();
            PoolManager.Instance.Push(this);
        }
    }
}
