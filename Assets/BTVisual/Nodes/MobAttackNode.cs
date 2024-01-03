using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttackNode : ActionNode
{
    private MobBrain _mBrain;

    protected override void OnStart()
    {
        brain.StopChase();
        _mBrain = (brain as MobBrain);
        _mBrain.Animator.SetIsMove(false);
        _mBrain.Animator.SetIsAttack(brain.attack.IsAttack = true);
        _mBrain.isAnimFinised = false;
        _mBrain.Animator.SetAttackTrigger(true);
    }

    protected override void OnStop()
    {
        _mBrain = (brain as MobBrain);
        _mBrain.Animator.SetIsMove(true);
        brain.StartChase();
        _mBrain.Animator.SetIsAttack(brain.attack.IsAttack = false);
    }

    protected override State OnUpdate()
    {
        if (_mBrain.isAnimFinised)
        {
            return State.SUCCESS;

        }
        else return State.RUNNING;
    }
}
