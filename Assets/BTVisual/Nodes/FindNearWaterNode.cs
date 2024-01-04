using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FindNearWaterNode : ActionNode
    {
        private float _timer;

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            _timer += Time.deltaTime;
            if(_timer >= 2f)
            {
                
            }
            return State.FAILURE;
        }
    }
}