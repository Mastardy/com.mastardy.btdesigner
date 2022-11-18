using UnityEngine;

namespace BTDesigner
{
    public class RootNode : Node
    {
        [SerializeReference] public Node child;

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
        
        protected override void OnStart() { }

        protected override NodeState OnUpdate() => child.Update();
        
        protected override void OnEnd() { }
    }
}
