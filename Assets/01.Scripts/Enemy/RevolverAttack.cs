using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverAttack : EnemyAttack
{
    private void AnimationEventHandle()
    {

    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        Transform playerTrm = GameManager.Instance.player;

        PoolableMono pam = PoolManager.Instance.Pop(PoolingType.RevolverEnemyBullet);
        pam.transform.position = _brain.firePos.position;
        Vector2 dir = (playerTrm.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        pam.transform.rotation = angleAxis;

        _attackTimer = 0;
    }
}
