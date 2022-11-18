using UnityEngine;

namespace BTDesigner
{
    [CreateAssetMenu(fileName = "Behavior Tree Design", menuName = "BTDesign/Behavior Tree Design")]
    public class BehaviorTreeDesign : ScriptableObject
    {
        public RootNode rootNode;
        [HideInInspector] public NodeState treeState = NodeState.Running;

        public NodeState Update()
        {
            if (rootNode.state == NodeState.Running) treeState = rootNode.Update();
            return treeState;
        }
    }    
}