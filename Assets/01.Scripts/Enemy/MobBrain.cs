using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBrain : EnemyBrain
{
    [SerializeField]
    private MobAnimator _animator;

    public MobAnimator Animator => _animator;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Init()
    {
        isDead = false;
    }

    public override void SetDead()
    {
        base.SetDead();
        _animator.SetDieTrigger(true);
    }

    public void OnHit()
    {
        SetDead();
        (this as IDamageable).OnHit();
    }
}
