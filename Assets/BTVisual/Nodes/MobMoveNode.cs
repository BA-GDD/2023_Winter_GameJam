using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMoveNode : ActionNode
{
    private MobBrain _brain;
    protected override void OnStart()
    {
        brain.StartChase();
        _brain = (brain as MobBrain);
        _brain.Animator.SetIsAttack(brain.attack.IsAttack = false);
        _brain.Animator.SetIsMove(true);
    }

    protected override void OnStop()
    {
        brain.rigidbody2d.velocity = new Vector2(0, 0);
    }

    protected override State OnUpdate()
    {
        if (_brain.status.atkRange < Vector2.Distance(_brain.transform.position, GameManager.Instance.player.position))
        {
            //_brain.characterController.Move(_brain.dir * _brain.status.moveSpeed * Time.deltaTime);
            brain.rigidbody2d.velocity = _brain.dir * _brain.status.moveSpeed;
            return State.RUNNING;
        }
        return State.SUCCESS;
    }
}
