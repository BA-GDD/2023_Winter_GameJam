using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverAttack : EnemyAttack
{
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        if (_attackTimer >= _brain.status.atkDelayTime && _isAttack)
        {
            Transform playerTrm = GameManager.Instance.player;

            Vector2 dir = (playerTrm.position - transform.position).normalized;
            PoolableMono pam = PoolManager.Instance.Pop(PoolingType.RevolverEnemyBullet);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
            pam.transform.rotation = angleAxis;
        }
    }
}
