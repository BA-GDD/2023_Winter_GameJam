using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearWaterNode : ActionNode
{
    private BomberManBrain _brain;
    private float _timer;

    protected override void OnStart()
    {
        Debug.Log(brain == null);
        brain.StartChase();
        _brain = (brain as BomberManBrain);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2f)
        {
            _brain.SetDir();
            _timer = 0f;
        }
        brain.rigidbody2d.velocity = _brain.dir * brain.status.moveSpeed;

        if (MapManager.Instance.CheckWater(brain.transform.position, out Vector3Int pos))
        {

            brain.SetDead(true);

            return State.SUCCESS;
        }

        return State.RUNNING;
    }
}
