using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : EnemyAttack
{
    private Transform _targetTrm;

    protected override void Update()
    {
        base.Update();
        _targetTrm
    }

    public override void Attack()
    {
        if (_attackTimer >= _brain.status.atkDelayTime && _isAttack)
        {
            RaycastHit2D hit;
            bool isHit = Physics2D.Raycast(transform.position, transform.forward, out hit);
        }
    }
}
