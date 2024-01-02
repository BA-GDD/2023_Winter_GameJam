using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMoveNode : ActionNode
{
    protected override void OnStart()
    {
        brain.StartChase();
        (brain as MobBrain).Animator.SetIsAttack(brain.attack.IsAttack = false);
        (brain as MobBrain).Animator.SetIsMove(true);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
