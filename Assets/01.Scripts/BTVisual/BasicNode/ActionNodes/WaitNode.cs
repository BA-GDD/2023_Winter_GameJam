
using UnityEngine;

namespace BTVisual
{
    public class WaitNode : ActionNode
    {
        public float duration = 1f;

        private float _startTime;
        protected override void OnStart()
        {
            //시작할때 _startTime에 현재시간을 저장하고
            _startTime = Time.time;
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            if(Time.time - _startTime > duration)
            {
                return State.SUCCESS;
            }
            return State.RUNNING;
        }
    }
}
