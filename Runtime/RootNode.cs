using UnityEngine;

namespace BTDesigner
{
    [CreateAssetMenu(fileName = "Start Node", menuName = "BTDesign/Start Node", order = 1)]
    public class RootNode : Node
    {
        [SerializeReference] public Node child;
        
        protected override void OnStart() { }

        protected override NodeState OnUpdate() => child.Update();
        
        protected override void OnEnd() { }
    }
}
