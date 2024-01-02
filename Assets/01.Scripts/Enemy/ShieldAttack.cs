using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : EnemyAttack
{
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        Collider2D collider = Physics2D.OverlapCircle(_brain.transform.position, 1.5f, 1 << 6);

        if (collider != null)
        {
            // 플레이어 사망 처리
            Debug.Log("hit");
        }

        _attackTimer = 0;
    }
}
