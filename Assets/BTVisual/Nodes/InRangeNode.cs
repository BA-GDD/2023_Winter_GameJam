using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeNode : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return Vector3.Distance(brain.transform.position, GameManager.Instance.player.position) > (brain as MobBrain).status.atkRange ? State.FAILURE : State.SUCCESS;
    }
}
