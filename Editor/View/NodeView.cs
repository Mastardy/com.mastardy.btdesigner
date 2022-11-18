using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BTDesigner
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> OnNodeSelected;
        public Node node;

        public Port input;
        public Port output;
        
        public sealed override string title
        {
            get => base.title;
            set => base.title = value;
        }
        
        public NodeView(Node node)
        {
            this.node = node;
            title = node.name;

            viewDataKey = node.guid;
            
            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateInputPorts()
        {
            if (node is ActionNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is CompositionNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is UtilityNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is RootNode)
            {
                
            }
            
            if (input == null) return;

            input.portName = "";
            inputContainer.Add(input);
        }

        private void CreateOutputPorts()
        {
            if (node is ActionNode)
            {
                
            }
            else if (node is CompositionNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if (node is UtilityNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (node is RootNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if (output == null) return;
            output.portName = "";
            outputContainer.Add(output);
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            if (OnNodeSelected == null) return;
            OnNodeSelected.Invoke(this);
        }
    }
}
