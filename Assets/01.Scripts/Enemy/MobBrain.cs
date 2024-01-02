using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // 총알 맞으면 이거 실행시켜주면 됨
    public void OnHit()
    {
        SetDead();
        (this as IDamageable).OnHit();
    }
}
