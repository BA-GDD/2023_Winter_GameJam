using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    protected override void Update()
    {
        base.Update();


    }

    public override void Attack()
    {
        if(_attackTimer  >= _brain.status.atkDelayTime && _isAttack)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _brain.status.atkRange, _playerLayerMask);

            if (collider != null)
            {
                //collider.GetComponent<Player>().TakeDamage();
            }

            _attackTimer = 0;
        }
    }
}
