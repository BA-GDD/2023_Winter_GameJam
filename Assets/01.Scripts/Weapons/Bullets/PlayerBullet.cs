using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField]
    private LayerMask _enemyLayerMask;
    private bool _isAlreadyHit;

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
