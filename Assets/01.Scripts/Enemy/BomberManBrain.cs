using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberManBrain : EnemyBrain
{
    [SerializeField]
    private MobAnimator _animator;

    public MobAnimator Animator => _animator;

    protected override void OnEnable()
    {
        base.OnEnable();
        IsDead = false;
        _animator.SetIsMove(false);
        _animator.SetIsAttack(false);
        _animator.SetAttackTrigger(false);
        _animator.SetShootTrigger(false);
        _animator.SetDieTrigger(false);
    }

    protected override void Update()
    {
        if (dir.x * transform.localScale.x < 0)
        {
            Flip();
        }
    }

    public void SetDir()
    {
        dir = MapManager.Instance.GetNearWater(transform.position) - transform.position;
        dir.Normalize();
    }

    public override void Init()
    {

    }

    public override void SetDead(bool isBomberMan = true)
    {
        base.SetDead(isBomberMan);
        _animator.SetDieTrigger(true);
        attack.Attack();
        EnemySpawner.Instance.DeSpawnEnemy(this);
    }

    public void OnHitHandle()
    {
        SetDead(true);
        (this as IDamageable).OnHit();
    }
}
