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
        if (!isMissileMode)
        {
            if (collision.TryGetComponent(out MobBrain brain))
            {
                brain.OnHitHandle();
                PoolManager.Instance.Push(this);
            }
        }
        else
        {
            if (collision == targetOfMissile)
            {
                collision.GetComponent<MobBrain>().OnHitHandle();

                targetOfMissile = null;
                isMissileMode = false;

                PoolManager.Instance.Push(this);
            }
        }
    }
}
