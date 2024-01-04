using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField]
    private LayerMask _enemyLayerMask;
    private bool _isAlreadyHit;

    private Action _killEvent;

    protected override void OnEnable()
    {
        base.OnEnable();

        _isAlreadyHit = false;
    }

    private void Update()
    {
        if (isMissileMode)
        {
            Vector2 direction = targetOfMissile.transform.position - transform.position;

            direction.Normalize();

            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);

            rigidbody2d.velocity = transform.right * bulletSpeed;
        }
    }

    public void ApeendEvent(Action action)
    {
        _killEvent += action;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isMissileMode)
        {
            if (1 << collision.gameObject.layer == _enemyLayerMask)
            {
                if (!_isAlreadyHit && collision.TryGetComponent(out MobBrain brain))
                {
                    brain.OnHitHandle();
                }

                _isAlreadyHit = true;

                _killEvent?.Invoke();
                PoolManager.Instance.Push(this);
            }
        }
        else
        {
            if (collision == targetOfMissile)
            {
                if(collision.TryGetComponent<MobBrain>(out MobBrain brain))
                    brain.OnHitHandle();

                targetOfMissile = null;
                isMissileMode = false;

                _killEvent?.Invoke();
                PoolManager.Instance.Push(this);
            }
        }
    }
    private void OnDisable()
    {
        _killEvent = null;
    }
}
