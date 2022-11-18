using UnityEngine;

namespace BTDesigner
{
    public class SequencerNode : CompositionNode
    {
        private int currentNode;
        
        protected override void OnStart()
        {
            currentNode = 0;
        }

        protected override NodeState OnUpdate()
        {
            if (children.Count == 0) return NodeState.Failure;
            
            var childState = children[currentNode].Update();

            if (childState != NodeState.Success) return childState;

            currentNode++;
            return currentNode == children.Count ? NodeState.Success : NodeState.Running;
        }

        protected override void OnEnd() { }
    }
}
