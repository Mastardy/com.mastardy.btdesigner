using UnityEngine;

namespace BTDesigner
{
    public class WaitLog : ActionNode
    {
        private float start;
        public float duration = 1.0f;
        
        protected override void OnStart()
        {
            start = Time.time;
            Debug.Log(Time.time);
        }

        protected override NodeState OnUpdate()
        {
            return Time.time - start < duration ? NodeState.Running : NodeState.Success;
        }

        protected override void OnEnd()
        {
            Debug.Log(Time.time);
        }
    }
}