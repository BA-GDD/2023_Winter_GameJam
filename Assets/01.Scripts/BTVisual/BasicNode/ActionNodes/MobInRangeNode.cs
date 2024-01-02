using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class MobInRangeNode : ActionNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            //return Vector2.Distance(brain.transform.position, GameManager.instance.)
            return State.FAILURE;
        }
    }
}
