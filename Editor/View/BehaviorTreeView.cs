using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BTDesigner
{
    public class BehaviorTreeView : GraphView
    {
        public Action<NodeView> OnNodeSelected;

        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, UxmlTraits> { }
        
        private BehaviorTreeDesign tree;
        
        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.mastardy.btdesigner/Editor/Resources/Styles/BehaviorTreeDesignEditorWindow.uss");
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }
        
        private void OnUndoRedo()
        {
            PopulateView(tree);
            AssetDatabase.SaveAssets();
        }

        private NodeView GetNodeView(Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }
        
        public void PopulateView(BehaviorTreeDesign treeDesign)
        {
            
            tree = treeDesign;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (treeDesign.rootNode == null)
            {
                treeDesign.rootNode = treeDesign.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(treeDesign);
                AssetDatabase.SaveAssets();
            }
            
            foreach (var treeNode in treeDesign.nodes)
            {
                CreateNodeView(treeNode);
            }

            foreach (var treeNode in treeDesign.nodes)
            {
                var children = treeDesign.GetChildren(treeNode);
                foreach (var child in children)
                {
                    var parentView = GetNodeView(treeNode);
                    var childView = GetNodeView(child);

                    var edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                }
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var element in graphViewChange.elementsToRemove)
                {
                    if (element is NodeView nodeView) tree.DeleteNode(nodeView.node);
                    if (element is Edge edge)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        tree.RemoveChild(parentView?.node, childView?.node);
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    var parentView = edge.output.node as NodeView;
                    var childView = edge.input.node as NodeView;
                    
                    tree.AddChild(parentView?.node, childView?.node);
                }
            }
            
            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //base.BuildContextualMenu(evt);

            var localMousePosition = viewTransform.matrix.inverse.MultiplyPoint(evt.localMousePosition);
            
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType?.Name}] {type.Name}", (_) => CreateNode(type, localMousePosition));
                }
            }
            
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositionNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType?.Name}] {type.Name}", (_) => CreateNode(type, localMousePosition));
                }
            }
            
            {
                var types = TypeCache.GetTypesDerivedFrom<UtilityNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType?.Name}] {type.Name}", (_) => CreateNode(type, localMousePosition));
                }
            }
        }

        private void CreateNode(Type type, Vector2 position)
        {
            Node node = tree.CreateNode(type);
            Debug.Log(position);
            CreateNodeView(node).SetPosition(new Rect(position, Vector2.zero));
        }
        
        private NodeView CreateNodeView(Node node)
        {
            var nodeView = new NodeView(node)
            {
                OnNodeSelected = OnNodeSelected
            };
            AddElement(nodeView);
            return nodeView;
        }
    }
}