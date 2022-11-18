using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BTDesigner
{
    [CreateAssetMenu(fileName = "Behavior Tree Design", menuName = "BTDesigner/Behavior Tree Design")]
    public class BehaviorTreeDesign : ScriptableObject
    {
        public Node rootNode;
        [HideInInspector] public NodeState treeState = NodeState.Running;

        public List<Node> nodes = new();
        
        public NodeState Update()
        {
            if (rootNode.state == NodeState.Running) treeState = rootNode.Update();
            return treeState;
        }

        public Node CreateNode(System.Type type)
        {
            Node node = CreateInstance(type) as Node;
            
            if (node == null) return null;
            
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            var composition = parent as CompositionNode;
            if (composition) composition.children.Add(child);

            var startNode = parent as RootNode;
            if (startNode) startNode.child = child;
            
            var utilNode = parent as UtilityNode;
            if (utilNode) utilNode.child = child;
        }

        public void RemoveChild(Node parent, Node child)
        {
            var composition = parent as CompositionNode;
            if (composition) composition.children.Remove(child);

            var startNode = parent as RootNode;
            if (startNode) startNode.child = null;
            
            var utilNode = parent as UtilityNode;
            if (utilNode) utilNode.child = null;
        }

        public List<Node> GetChildren(Node parent)
        {
            var children = new List<Node>();

            var utilityNode = parent as UtilityNode;
            if(utilityNode) children.Add(utilityNode.child);
            
            var startNode = parent as RootNode;
            if (startNode) children.Add(startNode.child);

            var composition = parent as CompositionNode;
            if (composition) return composition.children;
            
            return children;
        }

        public BehaviorTreeDesign Clone()
        {
            var tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            return tree;
        }
    }    
}