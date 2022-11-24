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
            
            Undo.RecordObject(this, "Behavior Tree (CreateNode)");
            nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (CreateNode)");
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behavior Tree (DeleteNode)");
            nodes.Remove(node);
            
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            var composition = parent as CompositionNode;
            if (composition)
            {
                Undo.RecordObject(composition, "Behavior Tree (AddChild)");
                composition.children.Add(child);
                EditorUtility.SetDirty(composition);
            }

            var startNode = parent as RootNode;
            if (startNode)
            {
                Undo.RecordObject(startNode, "Behavior Tree (AddChild)");
                startNode.child = child;
                EditorUtility.SetDirty(startNode);
            }
            
            var utilNode = parent as UtilityNode;
            if (utilNode)
            {
                Undo.RecordObject(utilNode, "Behavior Tree (AddChild)");
                utilNode.child = child;
                EditorUtility.SetDirty(utilNode);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            var composition = parent as CompositionNode;
            if (composition)
            {
                Undo.RecordObject(composition, "Behavior Tree (RemoveChild)");
                composition.children.Remove(child);
                EditorUtility.SetDirty(composition);
            }

            var startNode = parent as RootNode;
            if (startNode)
            {
                Undo.RecordObject(startNode, "Behavior Tree (RemoveChild)");
                startNode.child = null;
                EditorUtility.SetDirty(startNode);
            }
            
            var utilNode = parent as UtilityNode;
            if (utilNode)
            {
                Undo.RecordObject(utilNode, "Behavior Tree (RemoveChild)");
                utilNode.child = null;
                EditorUtility.SetDirty(utilNode);
            }
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